namespace College.Domain.Constants;

public static class BusinessMessages
{
    public const string StudentNameRequired = "O nome do aluno é obrigatório.";
    public const string StudentNameTooLong = "O nome deve ter no máximo 50 caracteres.";
    public const string InvalidEmailDomain = "O e-mail deve pertencer ao domínio @faculdade.edu.";
    public const string DuplicateEmail = "Já existe um aluno matriculado com este e-mail.";
    public const string StudentNotFound = "Aluno não encontrado.";
}
