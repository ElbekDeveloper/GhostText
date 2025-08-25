namespace GhostText.Services.Levenshteins;

public interface ILevenshteinService
{
    int LevenshteinDistance(string s, string t);
}