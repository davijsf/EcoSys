namespace EcoSys.Core.Services;

using EcoSys.Core.Entities;

public class TagService
{
    public Empresa? empresa {get;}

    public TagService(Empresa? empresa = null)
    {
        this.empresa = empresa;
    }

    public void CadastrarTag (string nomeTag)
    {
       if (empresa?.Tags.Any(c => 
       c.Nome.Equals(nomeTag, StringComparison.OrdinalIgnoreCase)) == true)
        {
            Console.WriteLine("Tag já cadastrada.");
            return;
        }

        // Cadastrar Tag, caso não exista ainda
        empresa?.Tags.Add(new Tag
        {
            Nome = nomeTag
        });
        Console.WriteLine("Tag cadastrada com sucesso.");
    }

    public List<Tag> ListarTags()
    {
        return empresa.Tags;
    }

    public Tag? BuscarTagsPorNome(string nome)
    {
        return empresa?.Tags.FirstOrDefault(t => 
        t.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }   

    public bool RemoverTag(string nome)
    {
        var tag = BuscarTagsPorNome(nome);

        if (tag != null)
        {
            empresa.Tags.Remove(tag);
            return true;
        }

        return false;
    }
}