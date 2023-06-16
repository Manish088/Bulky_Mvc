namespace DI_Seriver_Lifetime.Services
{
    public class SingletonGuidServices : ISingletonGuidServices 
    {
        private readonly Guid Id;
        public SingletonGuidServices()
        {
            Id = Guid.NewGuid();
        }
        public string getGuid()
        {
            return Id.ToString();
        }
    }
}
