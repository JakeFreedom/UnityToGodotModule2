using Godot;

public partial class MoleTarget : Node3D
{

	[Signal]
	public delegate void MoleTargerQueFreeEventHandler(MoleTarget mt);

	public float traveled = 0f;
	public int direction = 1;
	public int MaxHeight { get; set; }


	
	private void AreaEnteredHandler(Area3D otherArea) =>	EmitSignal(SignalName.MoleTargerQueFree, this);
	
	private void DeathTimerTimeOut() => this.QueueFree();
}
