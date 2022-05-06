using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UIElements;
using Random = System.Random;
using Slider = UnityEngine.UI.Slider;


namespace DefaultNamespace
{
    public class CreateFire : MonoBehaviour
    {
        public GameObject[] blocks;
        public GameObject fire;
        private bool waiting = true;
        private Timer waitingTimer = new Timer();
        Animator animator;
        private Random rnd;
        public Slider progressBar;
        private Timer activityTimer = new Timer();
        private float activityTime = 6.5f;

        private bool activated;
        private List<BlockState> BlockStates;
        public Material urpMaterial;


        // Start is called before the first frame update
        void Start()
        {
            print("start");
            BlockStates = new List<BlockState>();
            foreach (var block in blocks)
            {
                BlockStates.Add(new BlockState(block, fire,
                    block.transform.Find("FireSystem").gameObject.GetComponent<ParticleSystem>(), urpMaterial));
            }

            rnd = new Random();
            progressBar.GetComponent<RectTransform>().transform.localScale = new Vector3(0, 1, 0);
        }

        // Update is called once per frame
        void Update()
        {
            if (waiting)
            {
                waitingTimer.Update(Time.deltaTime);
                if (waitingTimer.Seconds >= 2)
                {
                    waitingTimer.Reset();
                    waiting = false;
                }
            }

            else
            {
                if (!activated)
                {
                    var firingBlocksCount = rnd.Next(blocks.Length / 2, blocks.Length - 1);
                    var blockIdxs = new List<int>();
                    for (var i = 0; i < blocks.Length; i++)
                        blockIdxs.Add(i);

                    for (var i = 0; i < firingBlocksCount; i++)
                    {
                        var j = rnd.Next(blockIdxs.Count);
                        BlockStates[blockIdxs[j]].Activate(rnd.Next());
                        blockIdxs.RemoveAt(j);
                    }

                    progressBar.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 0);
                    activated = true;
                }

                else
                {
                    activityTimer.Update(Time.deltaTime);
                    progressBar.value = activityTimer.Seconds / activityTime;

                    var activeBlocks = 0;
                    foreach (var blockState in BlockStates)
                    {
                        blockState.Update();
                        if (blockState.isActive || blockState.onDestroying) activeBlocks++;
                    }

                    if (activeBlocks != 0) return;
                    activated = false;
                    waiting = true;
                    activityTimer.Reset();
                    progressBar.GetComponent<RectTransform>().transform.localScale = new Vector3(0, 1, 0);
                    progressBar.value = 0;
                }
            }
        }


        private class BlockState
        {
            
            public bool isActive;
            private GameObject block, fire;
            private ParticleSystem fireSystem;
            private List<GameObject> activeFire;
            public bool onDestroying;
            private bool activatingFireSystem;
            private Timer fireSystemDelayTimer;
            private float stopTime;
            private float fireLightIntensivity;
            private Material defaultMaterial;
            private Material urpMaterial;

            public BlockState(GameObject block, GameObject fire, ParticleSystem fireSystem, Material urpMaterial)
            {
                this.block = block;
                defaultMaterial = block.GetComponent<SpriteRenderer>().material;
                this.fire = fire;
                this.fireSystem = fireSystem;
                this.fireSystem.Stop();
                fireSystemDelayTimer = new Timer();
                this.urpMaterial = urpMaterial;


                //block.GetComponent<SpriteRenderer>().material = new Material();
            }


            public void Activate(int seed)
            {
                var rnd = new Random(seed);
                var firingBlockPos = block.transform.position;
                activeFire = new List<GameObject>();
                block.GetComponent<SpriteRenderer>().material = urpMaterial;

                for (var dx = -4f; dx <= 4f; dx+=0.5f)
                {
                    if (rnd.Next(101) % 2 == 1) continue;
                    var obj = Instantiate(fire, firingBlockPos + new Vector3(dx, -0.8f, 0),
                        block.transform.rotation);
                    activeFire.Add(obj);
                }

                block.GetComponent<SpriteRenderer>().material = urpMaterial;
                isActive = true;
            }


            private void DestroyFire()
            {
                foreach (var fire in activeFire)
                    Destroy(fire);
                block.GetComponent<SpriteRenderer>().material = defaultMaterial;
            }

            public void Update()
            {
                if (onDestroying)
                {
                    if (fireLightIntensivity > 0)
                    {
                        foreach (var fire in activeFire)
                            fire.transform.Find("Point Light 2D").GetComponent<Light2D>().intensity -= 0.002f;
                        fireLightIntensivity -= 0.002f;
                    }

                    if (activatingFireSystem)
                    {
                        fireSystemDelayTimer.Update(Time.deltaTime);
                        if (fireSystemDelayTimer.Seconds < 2) return;
                        activatingFireSystem = false;
                        fireSystemDelayTimer.Reset();
                        foreach (var fire in activeFire)
                            fire.GetComponent<Animator>().SetBool("OnDestroying", true);
                        stopTime = activeFire[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0)
                            .normalizedTime;
                    }


                    var normalizedTime = activeFire[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0)
                        .normalizedTime;
                    if (normalizedTime < stopTime && normalizedTime >= 1)
                    {
                        DestroyFire();
                        fireSystem.Stop();
                        onDestroying = false;
                    }
                }

                if (!isActive) return;

                if (fireLightIntensivity < 3)
                {
                    foreach (var fire in activeFire)
                        fire.transform.Find("Point Light 2D").GetComponent<Light2D>().intensity += 0.01f;
                    fireLightIntensivity += 0.01f;
                }


                if (CheckClick(activeFire))
                {
                    isActive = false;
                    activatingFireSystem = true;
                    fireSystem.Play();
                    onDestroying = true;
                }
            }

            [CanBeNull]
            private bool CheckClick(List<GameObject> fires)
            {
                if (!Input.GetMouseButtonDown(0)) return false;

                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(mousePos, Vector2.zero);
                if (hit.collider == null) return false;

                foreach (var fire in fires)
                {
                    if (hit.collider.gameObject == fire) return true;
                }

                return false;
            }
        }
    }
}