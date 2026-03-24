namespace UI.Buttons
{
    public abstract class MakeUpButton : AbstractButton
    {
        protected int Index { get; private set; }

        public void SetIndex(int index)
        {
            Index = index;
        }
    
        protected override void OnClick()
        {
       
        }
    }
}
