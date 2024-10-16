using AutoMapper;
using MasterProjectDAL.DataModel;
using MasterProjectDTOModel.Product;
using MasterProjectDTOModel.Relation;
using MasterProjectDTOModel.school;
using MasterProjectDTOModel.User;
using MasterProjectDTOModel.UserMember;

namespace MasterProjectWebAPI.AutoMapperProfile
{
    public class MapperProfileDeclaration : Profile
    {
        public MapperProfileDeclaration() 
        {
            #region Product
            CreateMap<AddProductRequest_DTO, Product>();
            CreateMap<UpdateProductRequest_DTO, Product>();
            CreateMap<Product,GetProductResponse_DTO>();

            #endregion

            #region User
            CreateMap<AddUserRequest_DTO, User>();
            CreateMap<UpdateUserRequest_DTO, User>();
            CreateMap<User, GetUserResponse_DTO>();
            CreateMap<User, GetmemberChildListResponseList>();
            CreateMap<User, Userss>();

            #endregion

            #region UserMember
            CreateMap<AddUserMemberRequest_DTO, UserMember>();
            CreateMap<UpdateUserMemberRequest_DTO, UserMember>();
            CreateMap<UserMember, GetUserMemberResponse_DTO>();
            CreateMap<UserMember, GetmemberChildListResponseList>();
            CreateMap<UserMember, GetUserMemberList>();
            #endregion

            #region School
            CreateMap<AddSchoolRequest_DTO, School>();
            CreateMap<School, GetSchoolResponse_DTO>();

            #endregion

            #region Relation
            CreateMap<AddRelationRequest_DTO, Relation>();
            CreateMap<Relation, GetRelationResponse_DTO>();
            #endregion
        }
    }
}
