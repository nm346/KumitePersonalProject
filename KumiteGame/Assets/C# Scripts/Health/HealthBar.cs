/*Nathan */

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteInEditMode]
public class HealthBar : MonoBehaviour
{	
	public enum ColorMode
	{
		Single,
		Gradient
	}
	public ColorMode colorMode;
	public Image barImage;
	public Color barColor = Color.white;
	public Gradient barGradient = new Gradient();
	public enum DisplayText
	{
		
		Disabled,
		Percentage,
		CurrentValue,
		CurrentAndMaxValues
	}
	public DisplayText displayText;
	public Text barText;
	public string additionalText = string.Empty;
	public bool fillConstraint = false;
	public float fillConstraintMin = 0.0f;
	public float fillConstraintMax = 1.0f;
	float currentFraction = 1.0f;
	public float GetCurrentFraction
	{
		get
		{
			return currentFraction;
		}
	}
	float _maxValue = 0.0f;
	static Dictionary<string, HealthBar> SimpleHealthBars = new Dictionary<string, HealthBar>();
	public string barName = "";
	void Awake ()
	{
		if( Application.isPlaying == false )
			return;
		if( barName != string.Empty )
		{
			if( SimpleHealthBars.ContainsKey( barName ) )
				SimpleHealthBars.Remove( barName );

			SimpleHealthBars.Add( barName, this );
		}
	}
	public void UpdateBar ( float currentValue, float maxValue )
	{
		if( barImage == null )
			return;
		currentFraction = currentValue / maxValue;
		if( currentFraction < 0 || currentFraction > 1 )
			currentFraction = currentFraction < 0 ? 0 : 1;
		_maxValue = maxValue;
		barImage.fillAmount = fillConstraint == true ? Mathf.Lerp( fillConstraintMin, fillConstraintMax, currentFraction ) : currentFraction;
		if( colorMode == ColorMode.Gradient )
			barImage.color = barGradient.Evaluate( currentFraction );
		if( displayText != DisplayText.Disabled && barText != null )
		{
			switch( displayText )
			{
			case DisplayText.Percentage:
				{
					barText.text = additionalText + ( currentFraction * 100 ).ToString( "F0" ) + "%";
				}
				break;
			case DisplayText.CurrentValue:
				{
					barText.text = additionalText + ( currentFraction * _maxValue ).ToString( "F0" );
				}
				break;
			case DisplayText.CurrentAndMaxValues:
				{
					barText.text = additionalText + ( currentFraction * _maxValue ).ToString( "F0" ) + " / " + _maxValue.ToString();
				}
				break;
			}
		}
	}
	public void UpdateColor ( Color targetColor )
	{
		if( colorMode != ColorMode.Single || barImage == null )
			return;
		barColor = targetColor;
		barImage.color = barColor;
	}
	public void UpdateColor ( Gradient targetGradient )
	{
		if( colorMode != ColorMode.Gradient || barImage == null )
			return;
		barGradient = targetGradient;
		barImage.color = barColor;
	}

	public void UpdateTextColor ( Color targetColor )
	{
		if( displayText == DisplayText.Disabled || barText == null)
			return;

		barText.color = targetColor;
	}

	public static void UpdateBar ( string barName, float currentValue, float maxValue )
	{
		if( !SimpleHealthBarRegistered( barName ) )
			return;

		SimpleHealthBars[ barName ].UpdateBar( currentValue, maxValue );
	}
	public static void UpdateColor ( string barName, Color targetColor )
	{
		if( !SimpleHealthBarRegistered( barName ) )
			return;

		SimpleHealthBars[ barName ].UpdateColor( targetColor );
	}

	public static void UpdateColor ( string barName, Gradient targetGradient )
	{
		if( !SimpleHealthBarRegistered( barName ) )
			return;

		SimpleHealthBars[ barName ].UpdateColor( targetGradient );
	}
	public static void UpdateTextColor ( string barName, Color targetColor )
	{
		if( !SimpleHealthBarRegistered( barName ) )
			return;

		SimpleHealthBars[ barName ].UpdateTextColor( targetColor );
	}
	public static HealthBar GetSimpleHealthBar ( string barName )
	{
		if( !SimpleHealthBarRegistered( barName ) )
			return null;

		return SimpleHealthBars[ barName ];
	}

	static bool SimpleHealthBarRegistered ( string barName )
	{
		if( SimpleHealthBars.ContainsKey( barName ) )
			return true;

		return false;
	}
}