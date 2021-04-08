using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    public class LoadMetaData
    {
        private Type type;

        public LoadMetaData(Type type)
        {
            this.type = type;
        }

        public string LoadGeneralTypeInfo()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("-------------------------------------");
            builder.AppendLine("Generic Type Information");
            builder.AppendLine("--------------------------------------");
            builder.AppendLine($"Name : {type.Name}");
            builder.AppendLine($"Namespace : {type.Namespace}");
            builder.AppendLine($"Asse : {type.Namespace}");
            if (type.BaseType != null)
                builder.AppendLine($"Base type : {type.BaseType}");
            builder.AppendLine($"Is Public : {type.IsPublic}");
            builder.AppendLine($"Is Class : {type.IsClass}");
            builder.AppendLine($"Is Interface : {type.IsInterface}");
            builder.AppendLine($"Is Generic : {type.IsGenericType}");
            builder.AppendLine("----- Get Generic Args");
            foreach (var arg in type.GetGenericArguments())
            {
                builder.AppendLine($"       <{arg.Name}>");
            }
            builder.AppendLine("----- Implemented Interfaces");
            foreach (var inter in type.GetInterfaces())
            {
                builder.AppendLine($"        implemeted {inter.Name}");
            }

            return builder.ToString();
        }

        public string LoadProperties()
        {
            StringBuilder builder = new StringBuilder();
            PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            builder.AppendLine("-------------------------------------");
            builder.AppendLine("Type Properties");
            builder.AppendLine("--------------------------------------");
            foreach (var prop in propertyInfos)
            {
                if (prop.CanRead && prop.CanWrite)
                    builder.AppendLine($"public {prop.GetType().Name} {prop.Name} {{get; set;}}");
                else if (prop.CanRead && !prop.CanWrite)
                    builder.AppendLine($"public {prop.GetType().Name} {prop.Name} {{get;}}");
                else
                    builder.AppendLine($"public {prop.GetType().Name} {prop.Name} {{set;}}");

            }

            return builder.ToString();
        }

        public string LoadMethods()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("-------------------------------------");
            builder.AppendLine("Type Methods");
            builder.AppendLine("--------------------------------------");
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (var method in methods)
            {
                if(method.IsSpecialName) continue;
                builder.AppendLine($"public {method.ReturnType} {method.Name}");
                builder.Append("(");
                foreach (var parameter in method.GetParameters())
                {
                    builder.Append($"{parameter.ParameterType.Name} {parameter.Name}, ");
                }
                builder.Append(")");
            }

            return builder.ToString();
        }
    }

    public class LoadMetaData<T> : LoadMetaData
    {
        public LoadMetaData() : base(typeof(T))
        {
        }
    }
}
