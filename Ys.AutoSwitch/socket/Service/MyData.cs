using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.AutoSwitch
{
    public class MyData
    {

        private List<ClassModel> ClassModels = new List<ClassModel>();

        public void Refush(List<ClassModel> classes)
        {
            this.ClassModels.Clear();
            this.ClassModels = classes;
        }

        public ClassModel? Find(Func<ClassModel, bool> expression)
        {
            return ClassModels.FirstOrDefault(expression);
        }
        public ClassModel? Get(string host, int Port)
        {
            return ClassModels.FirstOrDefault(a => a.Host == host && a.Port == Port );
        }
        public ClassModel? GetIsMain(string host, int Port)
        {
            return ClassModels.FirstOrDefault(a => a.Host == host && a.Port == Port&&a.IsMain==true);
        }

        public ClassModel? GetIsNotMain(string host, int Port)
        {
            return ClassModels.FirstOrDefault(a => a.Host == host && a.Port == Port && a.IsMain == false);
        }

        public IEnumerable<ClassModel> NotMainAndEnable()
        {
            return ClassModels.Where(a => !a.IsMain && a.isEnable);
        }
    }
}
