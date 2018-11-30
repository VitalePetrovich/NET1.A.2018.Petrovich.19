using System;
using Ninject.Modules;
using Parser;
using Parser.Interfaces;
using Parser.Logger;

namespace DependencyResolver
{
    using Parser = Parser.Parser;

    /// <summary>
    /// Dependency Resolver class.
    /// </summary>
    public class DepResolver : NinjectModule
    {
        /// <summary>
        /// Method consist logic of bindings.
        /// </summary>
        public override void Load()
        {
            this.Bind<IDataProvider<string>>().To<FileStringsDataProvider>().WithConstructorArgument("SourceUrl.txt");
            this.Bind<IParserProvider<string, Uri>>().To<ToUriParse>();
            this.Bind<IValidator<string>>().To<StringToUrlValidator>();
            this.Bind<ISenderProvider<Uri>>().To<XmlFileSaver>().WithConstructorArgument("OutputXml.xml");
            this.Bind<ILogger>().To<NLogLogger>();
        }
    }
}
