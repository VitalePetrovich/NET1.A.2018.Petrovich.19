using DependencyResolver;
using Ninject;

namespace Parser.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new DepResolver());
            var parser = kernel.Get<Parser>();
            parser.Parse();
        }
    }
}
