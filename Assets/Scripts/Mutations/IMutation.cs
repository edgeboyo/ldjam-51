namespace Mutations
{
    /// <summary>
    /// Specifies what can be mutated by a mutation
    /// </summary>
    public interface IMutation<in TSubject>
    {
        public void Mutate(TSubject subject);
    }
}