using System;

public interface IInput
{
	public event Action LeftAttack;
	public event Action RightAttack;
}