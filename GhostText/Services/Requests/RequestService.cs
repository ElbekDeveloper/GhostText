using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GhostText.Services.Levenshteins;

namespace GhostText.Services.Requests
{
    public class RequestService : IRequestService
    {
        private readonly ILevenshteinService levenshteinService;
        private readonly List<string> dictionary;

        public RequestService(ILevenshteinService levenshteinService)
        {
            this.levenshteinService = levenshteinService;

            byte[] fileBytes = File.ReadAllBytes(Path.Combine("E://TestFiles", "yomonSoz.txt"));
            string fileContent = Encoding.UTF8.GetString(fileBytes);

            this.dictionary = fileContent
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim().ToLowerInvariant())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();
        }

        public bool ContainsForbiddenWord(string messageText)
        {
            if (string.IsNullOrWhiteSpace(messageText))
                return true; 

            string[] text = messageText
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToLowerInvariant())
                .ToArray();

            foreach (var input in text)
            {
                int bestDistance = int.MaxValue;
                int length = 0;

                foreach (var word in dictionary)
                {
                    int distance = this.levenshteinService.LevenshteinDistance(input, word);
                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        length = word.Length;
                    }
                }

                if (bestDistance <= 0.3 * length)
                    return false;
            }

            return true;
        }
    }
}
