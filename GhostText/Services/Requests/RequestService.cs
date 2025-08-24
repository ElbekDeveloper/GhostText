using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GhostText.Models;
using GhostText.Services.Levenshteins;

namespace GhostText.Services.Requests;
public class RequestService: IRequestService
{
    private readonly ILevenshteinService levenshteinService;
    private readonly List<string> dictionary;

    public RequestService(ILevenshteinService levenshteinService)
    {
        this.levenshteinService = levenshteinService;
        
        this.dictionary = File.ReadAllLines(
                Path.Combine("/Users/macbook/RiderProjects/GhostText/GhostText/bin/Debug/net9.0", "yomonSoz.txt"))
            .Select(x => x.Trim().ToLowerInvariant())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();
    }
    
    public bool ContainsForbiddenWord(string messageText)
    {
        if (string.IsNullOrWhiteSpace(messageText))
            return false;

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
                return true;
        }

        return false;
    }
}