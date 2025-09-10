interface IStore<T>
{
    static Task<T?> Load() => throw new NotImplementedException();
    static async Task Store(T modelName) => throw new NotImplementedException();
    static async Task Delete() => throw new NotImplementedException();
    static Task Update(T item) => throw new NotImplementedException();
}