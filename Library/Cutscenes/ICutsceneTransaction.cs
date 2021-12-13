using Library.Domain;

namespace Library.Cutscenes
{
    public interface ICutsceneTransaction
    {
        CutsceneTransactionType GetCutsceneTransactionType();
    }
}
