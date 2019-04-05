using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrudTest.TO;
using CrudTest.DAL;

namespace CrudTest.BLL
{
    public class UserBLL
    {
        ///<summary>Valida a regra de negócio de campos obrigatórios
        ///<param name="pUser">Objeto contendo os dados do usuário a ser cadastrado</param>
        ///<returns>key = booleano que informa se o objeto é valido ou não, value = string com a mensagem de retorno</returns>
        ///</summary>
        private KeyValuePair<bool, string> ValidateUser(UserTO pUser)
        {
            bool isValid = true;
            string strReturn = string.Empty;

            if (string.IsNullOrWhiteSpace(pUser.Name))
            {
                strReturn += !string.IsNullOrWhiteSpace(strReturn) ? Environment.NewLine : string.Empty;
                strReturn += "O campo 'Nome' é de preenchimento obrigatório";
                isValid = false;
            }
            else if (!pUser.Name.Trim().Contains(" "))
            {
                strReturn += !string.IsNullOrWhiteSpace(strReturn) ? Environment.NewLine : string.Empty;
                strReturn += "O campo 'Nome' deve ser preenchido com o nome completo";
                isValid = false;
            }


            if (string.IsNullOrWhiteSpace(pUser.Email))
            {
                strReturn += !string.IsNullOrWhiteSpace(strReturn) ? Environment.NewLine : string.Empty;
                strReturn += "O campo 'Email' é de preenchimento obrigatório";
                isValid = false;
            }

            return new KeyValuePair<bool, string>(isValid, strReturn);
        }

        ///<summary>Exclui um usuário pelo ID
        ///<param name="id">Id do usuário</param>
        ///<returns>key = booleano que informa se a função executou com sucesso ou não, value = string com a mensagem de retorno</returns>
        ///</summary>
        public KeyValuePair<bool, string> DeleteById(long pId)
        {
            string strReturn = string.Empty;
            bool boolReturn = false;

            try
            {
                UserDAL userDAL = new UserDAL();
                userDAL.DeleteById(pId);
                strReturn = "Usuário excluído com sucesso!";
                boolReturn = true;
            }
            catch (Exception ex)
            {
                strReturn = string.Format("Erro ao excluir o usuário! ({0})", ex.Message);
                boolReturn = false;
            }

            return new KeyValuePair<bool, string>(boolReturn, strReturn);
        }

        ///<summary>Lista todos os usuários
        ///<returns>Retorna uma lista com todos os usuários cadastrados. key = booleano que informa se a função executou com sucesso ou não, 
        ///value.key = string com a mensagem de retorno, value.value lista de usuários</returns>
        ///</summary>
        public KeyValuePair<bool, KeyValuePair<string, List<UserTO>>> GetAll()
        {
            string strReturn = string.Empty;
            bool boolReturn = false;
            List<UserTO> lstUser = null;

            try
            {
                UserDAL userDAL = new UserDAL();
                lstUser = userDAL.GetAll();
                strReturn = "OK";
                boolReturn = true;
            }
            catch (Exception ex)
            {
                strReturn = string.Format("Erro ao obter a lista de usuários! ({0})", ex.Message);
                boolReturn = false;
            }

            return new KeyValuePair<bool, KeyValuePair<string, List<UserTO>>>(boolReturn, 
                new KeyValuePair<string, List<UserTO>>(strReturn, lstUser));
        }

        ///<summary>Obtém um usuário pelo ID
        ///<param name="id">Id do registro que obtido.</param>
        ///<returns>Retorna um usuário pelo id. key = booleano que informa se a função executou com sucesso ou não, 
        ///value.key = string com a mensagem de retorno, value.value dados do usuário</returns>
        ///</summary>
        public KeyValuePair<bool, KeyValuePair<string, UserTO>> GetById(long pId)
        {
            string strReturn = string.Empty;
            bool boolReturn = false;
            UserTO user = null;

            if(pId > 0)
            {
                try
                {
                    UserDAL userDAL = new UserDAL();
                    user = userDAL.GetById(pId);
                    strReturn = "OK";
                    boolReturn = true;
                }
                catch (Exception ex)
                {
                    strReturn = string.Format("Erro ao obter a lista de usuários! ({0})", ex.Message);
                    boolReturn = false;
                }
            }
            else
            {
                strReturn = "Id não informado";
                boolReturn = false;
            }

            return new KeyValuePair<bool, KeyValuePair<string, UserTO>>(boolReturn,
                new KeyValuePair<string, UserTO>(strReturn, user));
        }

        ///<summary>Cadastra os dados de um usuário
        ///<param name="pUser">Objeto contendo os dados do usuário a ser cadastrado</param>
        ///<returns>key = booleano que informa se a função executou com sucesso ou não, value = string com a mensagem de retorno</returns>
        ///</summary>
        public KeyValuePair<bool, string> Save(UserTO pUser)
        {
            string strReturn = string.Empty;
            bool boolReturn = false;

            var validate = ValidateUser(pUser);

            if (validate.Key)
            {
                try
                {
                    UserDAL userDAL = new UserDAL();
                    userDAL.Save(pUser);
                    strReturn = "Usuário cadastrado com sucesso!";
                    boolReturn = true;
                }
                catch (Exception ex)
                {
                    strReturn = string.Format("Erro ao cadastrar o usuário! ({0})", ex.Message);
                    boolReturn = false;
                }
            }
            else
            {
                strReturn = validate.Value;
                boolReturn = false;
            }

            return new KeyValuePair<bool, string>(boolReturn, strReturn);
        }

        ///<summary>Atualiza os dados de um usuário
        ///<param name="pUser">Objeto contendo os dados do usuário a ser atualizado</param>
        ///<returns>key = booleano que informa se a função executou com sucesso ou não, value = string com a mensagem de retorno</returns>
        ///</summary>
        public KeyValuePair<bool, string> Update(UserTO pUser)
        {
            string strReturn = string.Empty;
            bool boolReturn = false;

            var validate = ValidateUser(pUser);

            if (pUser.Id > 0)
            {
                if (validate.Key)
                {
                    try
                    {
                        UserDAL userDAL = new UserDAL();
                        userDAL.Update(pUser);
                        strReturn = "Usuário atualizado com sucesso!";
                        boolReturn = true;
                    }
                    catch (Exception ex)
                    {
                        strReturn = string.Format("Erro ao atualizar o usuário! ({0})", ex.Message);
                        boolReturn = false;
                    }
                }
                else
                {
                    strReturn = validate.Value;
                    boolReturn = false;
                }
            }
            else
            {
                strReturn = "Id não informado";
                boolReturn = false;
            }

            return new KeyValuePair<bool, string>(boolReturn, strReturn);
        }
    }
}
