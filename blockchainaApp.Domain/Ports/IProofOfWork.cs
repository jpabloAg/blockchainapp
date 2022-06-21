using blockchainaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockchainaApp.Domain.Ports
{
    public interface IProofOfWork
    {
        string MakeProofOfWork(Block block);
    }
}
