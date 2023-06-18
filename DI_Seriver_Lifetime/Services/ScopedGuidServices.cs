namespace DI_Seriver_Lifetime.Services
{
    public class ScopedGuidServices : IScopedGuidServices
    {
        private readonly Guid Id;
        public ScopedGuidServices()
        {
            Id = Guid.NewGuid();
        }
        public string getGuid()
        {
            return  Id.ToString();
        }
    }
}
