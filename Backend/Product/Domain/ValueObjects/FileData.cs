using Microsoft.EntityFrameworkCore;

namespace Product.Domain.ValueObjects;

[Keyless]
public class FileData
{
    public string Name { get; set; }

    public long Size { get; set; }

    public string Type { get; set; }

    public string ObjectURL { get; set; }

    public DateTime LastModified { get; set; }

}
