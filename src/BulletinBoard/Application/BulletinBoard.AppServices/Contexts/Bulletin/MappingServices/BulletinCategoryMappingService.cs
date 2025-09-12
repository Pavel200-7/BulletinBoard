using BulletinBoard.AppServices.Contexts.Bulletin.MappingServices.IMappingServices;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.MappingServices;

public class BulletinCategoryMappingService : IBulletinCategoryMappingService
{
    public Task<BulletinCategoryReadAllDto> ConvertToBulletinCategoryReadAllDto(IReadOnlyCollection<BulletinCategoryDto> categories)
    {
        var root = CreateRootReadAllDtoNode();
        BuildReadAllDtoTree(root, categories.ToList(), null);
        return Task.FromResult(root);
    }

    private void BuildReadAllDtoTree
        (
            BulletinCategoryReadAllDto parent,
            List<BulletinCategoryDto> allCategories,
            Guid? parentId
        )
    {
        var children = allCategories
            .Where(c => c.ParentCategoryId == parentId)
            .OrderBy(c => c.CategoryName)
            .ToList();

        foreach (var childDto in children)
        {
            var childNode = MapToReadAllDto(childDto);
            parent.ChildrenCategories.Add(childNode);

            if (!childDto.IsLeafy)
            {
                BuildReadAllDtoTree(childNode, allCategories, childDto.Id);
            }
        }
    }

    private BulletinCategoryReadAllDto CreateRootReadAllDtoNode()
    {
        return new()
        {
            Id = null,
            ParentCategoryId = null,
            CategoryName = "Root",
            IsLeafy = false,
            ChildrenCategories = new List<BulletinCategoryReadAllDto>()
        };
    }

    private BulletinCategoryReadAllDto MapToReadAllDto(BulletinCategoryDto dto)
    {
        return new()
        {
            Id = dto.Id,
            ParentCategoryId = dto.ParentCategoryId,
            CategoryName = dto.CategoryName,
            IsLeafy = dto.IsLeafy,
            ChildrenCategories = new List<BulletinCategoryReadAllDto>()
        };
    }



    public Task<BulletinCategoryReadSingleDto> ConvertToBulletinCategoryReadSingleDto(IReadOnlyCollection<BulletinCategoryDto> categoriesChain)
    {
        if (!categoriesChain.Any()) { return Task.FromResult<BulletinCategoryReadSingleDto>(null); }

        var reversedChain = categoriesChain.Reverse().ToList();
        BulletinCategoryReadSingleDto root = MapToReadSingleDto(reversedChain[0]);
        BulletinCategoryReadSingleDto current = root;

        for (int i = 1; i < reversedChain.Count; i++)
        {
            var node = MapToReadSingleDto(reversedChain[i]);
            current.ChildrenCategory = node;
            current = node;
        }

        return Task.FromResult(root);
    }

    private BulletinCategoryReadSingleDto MapToReadSingleDto(BulletinCategoryDto dto)
    {
        return new()
        {
            Id = dto.Id,
            ParentCategoryId = dto.ParentCategoryId,
            CategoryName = dto.CategoryName,
            IsLeafy = dto.IsLeafy,
            ChildrenCategory = null
        };
    }
}
