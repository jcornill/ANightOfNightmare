public class ArgType<T> : Arg 
{
	public T value{ get; private set; }

	public ArgType(T pValue)
	{
		value = pValue;
	}
}




