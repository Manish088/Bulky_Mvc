namespace DI_Seriver_Lifetime.Services
{
    public class TransientGuidServices : ITransientGuidServices
    {
        private readonly Guid Id;
        public TransientGuidServices()
        {
            Id = Guid.NewGuid();
        }
        public string getGuid()
        {
            return  Id.ToString();
        }
    }
}
