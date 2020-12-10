using System.ComponentModel.DataAnnotations;

namespace ChessWeb.Application.Stuff
{
    public class OwnRequired : RequiredAttribute
    {
        public OwnRequired(string fieldName = "")
        {
            ErrorMessage = string.IsNullOrEmpty(fieldName) || string.IsNullOrWhiteSpace(fieldName)
                ? "Пж, поле надобно заполнить"
                : $"Пж, поле '{fieldName}' надобно заполнить";
        }
    }
}