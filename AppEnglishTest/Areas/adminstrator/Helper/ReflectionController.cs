using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AppEnglishTest.Areas.administrator.Helper
{
    public class ReflectionController
    {
        // Get controller
        public static List<Type> GetControllers(string namesapce)
        {
            List<Type> ListController = new List<Type>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = assembly.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type) &&
            type.Namespace.Contains(namesapce)).OrderBy(x => x.Name);
            return types.ToList();
        }
        //Get action
        public static List<string> GetActions(Type controller)
        {
            List<string> ListNameActions = new List<string>();
            IEnumerable<MemberInfo> memberInfos = controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public).
            Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any()).OrderBy(x => x.Name);
            foreach (MemberInfo method in memberInfos)
            {
                if (method.ReflectedType.IsPublic && !method.IsDefined(typeof(NonActionAttribute)))
                {
                    ListNameActions.Add(method.Name.ToString());
                }
            }
            return ListNameActions;
        }
    }
}