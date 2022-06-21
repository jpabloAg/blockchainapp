using blockchainaApp.Domain.Ports;
using System.Collections.Generic;
using System.Linq;

namespace blockchainaApp.Infrastructure.Adapters
{
    public class MerkelTreeHashing : IMerkelTreeHashing
    {
        private readonly IEncrypt _encrypt;
        public MerkelTreeHashing(IEncrypt encrypt)
        {
            _encrypt = encrypt;
        }

        public string GetMerkelTreeRootHash(List<string> transactionsHashes)
        {
            /*
             * De manera recursiva se agruparán los hashes de las transacciones de dos en dos, para luego concatenar cada par 
             * de hashes y computar un nuevo hash para cada grupo, hasta que solo quede un hash
             */
            var countHashes = transactionsHashes.Count();
            var lastHash = "";

            if(countHashes % 2 != 0)
            {
                lastHash = transactionsHashes.Last();
                transactionsHashes.RemoveAt(countHashes - 1);
                countHashes = transactionsHashes.Count();
            }

            var index = RangeIterator(0, countHashes, 2).ToList(); // 0,6,2 => 0,2,4
            var groupHashes = index.Select(x => transactionsHashes.GetRange(x,2));

            var mergeHashes = new List<string>();
            foreach (var group in groupHashes)
            {
                mergeHashes.Add(_encrypt.GetSHA256(group[0] + group[1]));
            }

            if (!string.IsNullOrEmpty(lastHash)) mergeHashes.Add(lastHash);

            if (mergeHashes.Count() == 1) return mergeHashes[0];

            return GetMerkelTreeRootHash(mergeHashes);
        }

        private IEnumerable<int> RangeIterator(int start, int stop, int step)
        {
            int x = start;
            do
            {
                yield return x;
                x += step;
                if (step < 0 && x <= stop || 0 < step && stop <= x)
                    break;
            }
            while (true);
        }
    }
}
