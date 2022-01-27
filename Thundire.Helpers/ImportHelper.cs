using System;

namespace Thundire.Helpers
{
    public class ImportHelper<TModel, TId>
    {
        public ImportHelper(Action<TId> updateSourceId, TModel model)
        {
            UpdateSourceIdAction = updateSourceId;
            Model = model;
        }

        public TModel Model { get; }
        public Action<TId> UpdateSourceIdAction { get; }

        public void UpdateSourceId(TId id) => UpdateSourceIdAction.Invoke(id);
    }
}