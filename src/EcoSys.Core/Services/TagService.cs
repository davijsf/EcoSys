namespace EcoSys.Core.Services;

using EcoSys.Core.Entities;

public class TagService
{
    private readonly Empresa _empresa; 

    public TagService(Empresa empresa)
    {
        _empresa = empresa ?? throw new ArgumentNullException(nameof(empresa));
    }

    public void CadastrarTag (string nomeTag)
    {
       if (_empresa?.Tags.Any(c => 
       c.Nome.Equals(nomeTag, StringComparison.OrdinalIgnoreCase)) == true)
        {
            Console.WriteLine("Tag já cadastrada.");
            return;
        }

        // Cadastrar Tag, caso não exista ainda
        _empresa?.Tags.Add(new Tag
        {
            Nome = nomeTag
        });
        Console.WriteLine("Tag cadastrada com sucesso.");
    }

    public List<Tag> ListarTags()
    {
        return _empresa.Tags;
    }

    public Tag? BuscarTagsPorNome(string nome)
    {
        return _empresa?.Tags.FirstOrDefault(t => 
        t.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }   

    public bool RemoverTag(string nome)
    {
        var tag = BuscarTagsPorNome(nome);

        if (tag != null)
        {
            _empresa.Tags.Remove(tag);
            return true;
        }

        return false;
    }
}