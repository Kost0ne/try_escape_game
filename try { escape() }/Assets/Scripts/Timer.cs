namespace DefaultNamespace
{
    public class Timer
    {
        public float Seconds { get; private set; }


        public void Update(float dt)
        {
            Seconds += dt;
        }

        public void Reset()
        {
            Seconds = 0;
        }
        
    }
    
    
    
}