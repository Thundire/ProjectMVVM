namespace Thundire.Helpers
{
    public interface IValidation<in TModel>
    {
        Result Validate(TModel model);
    }
}