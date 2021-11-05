using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    [ServiceContract]
    public interface WCFServiceInterface
    {
        [OperationContract]
        int Add(int x, int y);

        [OperationContract]
        string Concat(string x, double y);

        [OperationContract]
        A Sum(A x, A y);
    }
}
