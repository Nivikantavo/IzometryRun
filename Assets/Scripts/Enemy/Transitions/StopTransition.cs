
public class StopTransition : Transition
{
    private Exit _exit;

    private void Awake()
    {
        _exit = FindObjectOfType<Exit>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _exit.LevelCompleted += NeedStop;
    }

    private void OnDisable()
    {
        _exit.LevelCompleted -= NeedStop;
    }

    private void NeedStop(bool complited)
    {
        NeedTransit = complited;
    }
}
