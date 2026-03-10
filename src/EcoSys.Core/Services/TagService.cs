namespace EcoSys.Core.Services;

using EcoSys.Core.Entities;

public class TagService
{
    private List<Tag> tags = new List<Tag>();

    public void CadastrarTag (string nomeTag)
    {
        foreach (var tag in tags)
        {
            if (tag.Nome.ToLower() == nomeTag.ToLower())
            {
                Console.WriteLine("Tag já cadastrada.");
                return;
            }
        }

        // Cadastrar Tag, caso não exista ainda
        Tag novaTag = new Tag
        {
            Id = tags.Count + 1,
            Nome = nomeTag
        };

        tags.Add(novaTag);

        Console.WriteLine("Tag cadastrada com sucesso.");
    }

    public List<Tag> ListarTags()
    {
        return tags;
    }

    public Tag? BuscarTagsPorNome(string nome)
    {
        foreach (var tag in tags)
        {
            if (tag.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
            {
                return tag;
            }
        }
        return null;
    }   

    public bool RemoverTag(string nome)
    {
        var tag = BuscarTagsPorNome(nome);

        if (tag != null)
        {
            tags.Remove(tag);
            return true;
        }

        return false;
    }
}