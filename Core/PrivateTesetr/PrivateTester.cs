using Core.PrivateTester;
using System;
using System.Reflection;

namespace Core
{
    public interface IPrivateTester
    {
        Type GetPrivateType();
        void InvokePrivateMethod(string methodName);
        void InvokePrivateMethod(string methodName, params object[] parameters);
        string GetErrorMessage();
        object GetPrivateField(object instance, string fieldName);
        void SetPrivateField<ValueT>(object @object, string fieldName, ValueT v);
    }

    public class PrivateTester<T>:IPrivateTester where T:class
    {
        private ErrorMessage errorMessage;
        private IPrivateMethodFinder privateMethodFinder;

        public PrivateTester()
        {
        }

        public PrivateTester(IPrivateMethodFinder privateMethodFinder)
        {
            this.privateMethodFinder = privateMethodFinder;
        }

        public Type GetPrivateType()
        {
            return typeof(T);
        }

        public string GetErrorMessage()
        {
            return errorMessage.ToString();
        }

        public void InvokePrivateMethod(string methodName)
        {
            InvokePrivateMethod(methodName, new object[] { });
        }

        public void InvokePrivateMethod(string methodName, params object[] parameters)
        {
            var targetMethod = privateMethodFinder.GetPrivateMethod(methodName);
            var expecedParameterNumber = targetMethod.GetParameters().Length;
            var providedParameterNumber = parameters.Length;

            if (providedParameterNumber != expecedParameterNumber)
            {
                errorMessage.ParametersExpected = expecedParameterNumber;
                errorMessage.ParametersProvided = providedParameterNumber;
                throw new InvalidArgumentListException(GetErrorMessage());
            }

            var instance = Activator.CreateInstance(typeof(T));
            targetMethod.Invoke(instance, parameters);
        }

        public object GetPrivateField(object instance, string fieldName)
        {
            var privateType = typeof(T);

            var targetMember = privateType.GetMember(
                fieldName,
                BindingFlags.NonPublic | BindingFlags.Instance
                );

            if (targetMember.Length == 0)
                throw new PrivatePropertyException();

            var value = privateType
                .InvokeMember(
                    fieldName,
                    BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance,
                    null,
                    instance,
                    null
                    );

            return value;
        }

        public void SetPrivateField<ValueT>(object instance, string fieldName, ValueT value)
        {
            var privateType = typeof(T);

            var targetField = privateType.GetField(
                fieldName,
                BindingFlags.NonPublic | BindingFlags.Instance
                );

            if (targetField == null)
                throw new PrivatePropertyException(
                    $"Field {fieldName.ToUpper()} not found"
                    );

            if (targetField.FieldType != typeof(ValueT))
                throw new PrivatePropertyException(
                    $"Invalid type of argument provided. Expected: {targetField.FieldType}." +
                    $"Provided: {typeof(ValueT)}"
                    );
            
            targetField.SetValue(instance, value);
        }
    }
}
