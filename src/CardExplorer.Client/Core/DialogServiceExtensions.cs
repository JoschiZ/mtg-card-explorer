using Microsoft.AspNetCore.Components;
using MudBlazor;
using CardExplorer.Client.Features.Collections;

namespace CardExplorer.Client.Core;

internal static class DialogServiceExtensions
{
    extension(IDialogService dialogService)
    {
        public Task<CardCollectionDto?> CreateCollectionAsync()
            => dialogService.GetDataFromDialogAsync<CreateCollectionDialog, CardCollectionDto>();
        
        private async Task<T?> GetDataFromDialogAsync<TDialog, T>(DialogParameters? dialogParameters = null, DialogOptions? dialogOptions = null) where TDialog : IComponent
        {
            dialogParameters ??= new DialogParameters();
            var dialogRef = await dialogService.ShowAsync<TDialog>(null, dialogParameters, dialogOptions);
            var dialogResult = await dialogRef.Result;

            if (dialogResult is not { Canceled: false})
            {
                return default;
            }

            if (dialogResult.Data is null)
            {
                return default;
            }

            if (dialogResult.Data is not T resultData)
            {
                throw new InvalidOperationException($"Got an unexpected result type {dialogResult.Data?.GetType()} instead of {typeof(T)}");
            }
            
            return resultData;
        }
    }
}