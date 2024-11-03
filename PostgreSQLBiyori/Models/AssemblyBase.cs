using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQLBiyori.Models
{
    public abstract class AssemblyBase
    {
        public AssemblyItem ToAssemblyItem()
        {
            return new AssemblyItem() { FullName = this.FullName };
        }
        protected AssemblyBase(Assembly asm)
        {
            this.Assembly = asm;
            this.IsSelected = false;
        }
        protected AssemblyBase(string path)
        {
            this.Assembly = System.Reflection.Assembly.LoadFile(path);
            this.IsSelected = false;
        }

        public abstract bool IsGlobal { get; }
        public bool IsSelected { get; set; }
        public string Name
        {
            get
            {
                return this.Assembly.GetName().Name;
            }
        }
        public string FullName
        {
            get
            {
                return this.Assembly.FullName;
            }
        }
        public Assembly Assembly
        {
            get;
        }
    }
}
