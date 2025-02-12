//using CodeD.Domain.Abstractions;
//using Microsoft.EntityFrameworkCore.Storage;

//namespace CodeD.Infrastructure.Data;

//public class UnitOfWork : IUnitOfWork
//{
//    private readonly CodeDDbContext _context;
//    private readonly bool _withTransaction;
//    private readonly IDbContextTransaction? _transaction;
//    private bool disposedValue;

//    public UnitOfWork(CodeDDbContext context, bool withTransaction = true)
//    {
//        _context = context;
//        _withTransaction = withTransaction;
//        if (_withTransaction) _transaction = _context.Database.BeginTransaction();
//    }

//    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
//    {
//        var result = await _context.SaveChangesAsync(cancellationToken);

//        if (_withTransaction) await _transaction!.CommitAsync(cancellationToken);

//        return result;
//    }

//    protected virtual void Dispose(bool disposing)
//    {
//        if (!disposedValue)
//        {
//            if (disposing)
//            {
//                if (_withTransaction) _transaction!.Dispose();
//                _context.Dispose();
//            }

//            disposedValue = true;
//        }
//    }

//    public void Dispose()
//    {
//        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
//        Dispose(disposing: true);
//        GC.SuppressFinalize(this);
//    }
//}