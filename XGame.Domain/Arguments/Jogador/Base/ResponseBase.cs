namespace XGame.Domain.Arguments.Jogador.Base
{
    public class ResponseBase
    {
        public ResponseBase()
        {
            Message = "Ação executada com sucesso";
        }
        public string Message { get; set; }
    }
}
