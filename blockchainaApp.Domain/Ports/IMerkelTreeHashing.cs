using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockchainaApp.Domain.Ports
{
    public interface IMerkelTreeHashing
    {
        string GetMerkelTreeRootHash(List<string> transactionsHashes);
    }
}
