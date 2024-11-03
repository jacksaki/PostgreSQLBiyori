namespace PostgreSQLBiyori.Models
{
    public class LoadedAssembly : AssemblyBase
    {
        public LoadedAssembly(string path) : base(path)
        {
            this.Path = path;
        }

        public string Path
        {
            get;
        }
        public override bool IsGlobal
        {
            get
            {
                return false;
            }
        }
    }
}
