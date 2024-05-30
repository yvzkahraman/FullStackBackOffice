namespace BackOffice.Dtos
{
    public record ProductCreateDto(string Name, decimal Price, int Stock);
    public record ProductListDto(int Id, string Name, decimal Price);

    public record ProductUpdateDto(int Id, string Name,decimal Price,int Stock);
}