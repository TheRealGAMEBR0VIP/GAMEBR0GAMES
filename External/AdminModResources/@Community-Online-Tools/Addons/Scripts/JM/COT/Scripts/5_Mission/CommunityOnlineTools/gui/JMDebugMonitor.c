class JMDebugMonitor
{
	protected Widget layoutRoot;
	protected TextWidget WindowLabelText;
	protected TextListboxWidget NamesListboxWidget;
	protected TextListboxWidget ValuesListboxWidget;
	protected MultilineTextWidget ModifiersMultiTextWidget;
		
	void JMDebugMonitor()
	{		
		layoutRoot = GetGame().GetWorkspace().CreateWidgets("GUI/layouts/debug/day_z_debug_monitor.layout");
		layoutRoot.Show(false);
		
		WindowLabelText = TextWidget.Cast( layoutRoot.FindAnyWidget("WindowLabel") );
		NamesListboxWidget = TextListboxWidget.Cast( layoutRoot.FindAnyWidget("NamesListboxWidget") );
		ValuesListboxWidget = TextListboxWidget.Cast( layoutRoot.FindAnyWidget("ValuesListboxWidget") );
		ModifiersMultiTextWidget = MultilineTextWidget.Cast( layoutRoot.FindAnyWidget("ModifiersMultilineTextWidget") );
	}
	
	void Init()
	{
		#ifdef COT_DEBUGLOGS
		Print( "+" + this + "::Init" );
		#endif

		NamesListboxWidget.AddItem("HEALTH:", NULL, 0);
		ValuesListboxWidget.AddItem("", NULL, 0);

		NamesListboxWidget.AddItem("BLOOD:", NULL, 0);
		ValuesListboxWidget.AddItem("", NULL, 0);
		
		NamesListboxWidget.AddItem("LAST DAMAGE:", NULL, 0);
		ValuesListboxWidget.AddItem("", NULL, 0);
		
		NamesListboxWidget.AddItem("POSITION:", NULL, 0);
		ValuesListboxWidget.AddItem("", NULL, 0);
	
		layoutRoot.Show(true);

		#ifdef COT_DEBUGLOGS
		Print( "-" + this + "::Init" );
		#endif
	}

	void SetHealth(float value)
	{
		string health = string.Format(" %1", value.ToString());
		ValuesListboxWidget.SetItem(0, health, NULL, 0);
	}

	void SetBlood(float value)
	{
		string blood = string.Format(" %1", value.ToString());
		ValuesListboxWidget.SetItem(1, blood, NULL, 0);
	}
	
	void SetLastDamage(string value)
	{
		string lastDamage = string.Format(" %1", value);
		ValuesListboxWidget.SetItem(2, lastDamage, NULL, 0);
	}
	
	void SetPosition(vector value)
	{
		string position = string.Format(" %1 %2 %3", value[0].ToString(), value[1].ToString(), value[2].ToString());
		ValuesListboxWidget.SetItem(3, position, NULL, 0);
	}

	void Hide()
	{
		#ifdef COT_DEBUGLOGS
		Print( "+" + this + "::Hide" );
		#endif

		layoutRoot.Show(false);

		#ifdef COT_DEBUGLOGS
		Print( "-" + this + "::Hide" );
		#endif
	}

	void Show()
	{
		#ifdef COT_DEBUGLOGS
		Print( "+" + this + "::Show" );
		#endif

		layoutRoot.Show(true);

		#ifdef COT_DEBUGLOGS
		Print( "-" + this + "::Show" );
		#endif
	}
};