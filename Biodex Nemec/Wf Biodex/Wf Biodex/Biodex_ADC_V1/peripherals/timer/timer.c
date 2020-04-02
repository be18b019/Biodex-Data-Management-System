# include "timer.h"

void __attribute__((interrupt, auto_psv)) _T1Interrupt (void)
{	
		AD1CON1bits.ADON = 1; // enable ADC
		_T1IF = 0; // Reset InteruptFlag
}

void timer1Init(void)
{
	//Define a timer
	T1CON = 0; 		//stops and reset controllreg
	TMR1 = 0; 		//clears content of time register
	PR1 = 1000; 		//timer period value -> activates interrupt
	
	T1CONbits.TCKPS = 0b00; //intern clock prescale 1:1
	T1CONbits.TCS = 0; 		//using internal clock
	T1CONbits.TSYNC = 0; 	//external clock disabled

	T1CONbits.TON = 1; 		//timer bit is on
	
	_T1IF = 0;
	_T1IE = 1;
}

void __attribute__((interrupt, auto_psv)) _T2Interrupt (void)
{	
		LATAbits.LATA0 ^= 1; // enable ADC
		_T2IF = 0; // Reset InteruptFlag
}

void timer2Init(void)
{
	//Define a timer
	T2CON = 0; 		//stops and reset controllreg
	TMR2 = 0; 		//clears content of time register
	PR2 = 65535; 		//timer period value -> activates interrupt
	
	T2CONbits.TCKPS = 0b11; //intern clock prescale 1:256
	T2CONbits.TCS = 0; 		//using internal clock

	T2CONbits.TON = 1; 		//timer bit is on
	
	_T2IF = 0;
	_T2IE = 1;
}
