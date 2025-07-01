using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shiftly.Domain.Entities;

namespace Shiftly.Persistence.Configurations;

public class EventEntityConfiguration : IEntityTypeConfiguration<EventEntity>
{
    public void Configure(EntityTypeBuilder<EventEntity> builder)
    {
        builder.HasKey(rt => rt.Id);
		
        builder.Property(rt => rt.Id).ValueGeneratedNever();
        
        builder.Property(rt => rt.StreamId)
            .IsRequired();
        
        builder.Property(rt => rt.Type)
            .IsRequired();
        
        builder.Property(rt => rt.Data)
            .IsRequired();
        
        builder.Property(rt => rt.TimestampUtc)
            .IsRequired();
    }
}