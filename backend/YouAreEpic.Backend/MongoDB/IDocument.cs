using System;

namespace YouAreEpic.Backend.MongoDB
{
    public interface IDocument<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }

        DateTime CreatedAt { get; }
    }
}
