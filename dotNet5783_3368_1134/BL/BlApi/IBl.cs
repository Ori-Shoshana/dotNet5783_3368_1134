using BO;
namespace BlApi
{
    public interface IBl
    {
        public IBoCart Cart { get; }
        public IBoOrder Order { get; }
        public IBoProduct Product { get; }
    }
}
