namespace BlogApp.Models
{
    public abstract class Entity
    {
        //Identificador único gerado automaticamente na criação da entidade
        public Guid Id { get;  set; }

        //Data e hora em UTC da criação da entidade
        public DateTime Created { get;  set; }

        //Data e hora em UTC da atualização inicializada na criação
        public DateTime Updated { get;  set; }

        
        public Entity()
        {

            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;


        }




    }
}
