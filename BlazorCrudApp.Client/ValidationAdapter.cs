using Microsoft.AspNetCore.Components.Forms;

namespace BlazorCrudApp.Client;

public sealed class ValidationAdapter : IDisposable
{
    private readonly EditContext _context;
    private readonly ValidationMessageStore _store;
    
    public ValidationAdapter(
        EditContext context/*, 
        IReadOnlyDictionary<string, string[]> errorMap*/)
    {
        _context = context;
        _store = new ValidationMessageStore(context);
        
        _context.OnValidationRequested += OnValidate;
        _context.OnFieldChanged += OnFieldChanged;
    }
    
    private void OnValidate(object? sender, ValidationRequestedEventArgs args) 
        => _store.Clear();
    
    private void OnFieldChanged(object? sender, FieldChangedEventArgs args)
        => _store.Clear(args.FieldIdentifier);
    
    public void AddErrors(IReadOnlyDictionary<string, string[]> errors)
    {
        _store.Clear();
        
        foreach (var (name, messages) in errors)
        {
            var field = new FieldIdentifier(_context.Model, name);
            _store.Add(field, messages);
        }
        
        _context.NotifyValidationStateChanged();
    }
    
    public void Dispose()
    {
        _context.OnValidationRequested -= OnValidate;
        _context.OnFieldChanged -= OnFieldChanged;
    }
}