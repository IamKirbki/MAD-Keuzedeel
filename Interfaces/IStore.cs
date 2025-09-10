interface IStore<T>
{
    static Task<T?> Load() => throw new NotImplementedException();
    static async Task Store(T modelName) => throw new NotImplementedException();
    static async Task<bool> Delete() => throw new NotImplementedException();
}