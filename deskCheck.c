//This is to desk check calculation of new topics to study depending on
//number of repetitions scheduled for a day.

#include <stdio.h>

#define LOW_Y      0
#define HIGH_Y     2
#define RUN        3

/* count lines, words, and characters in input */
int main(void)
{
    float cur_reps, rise, slope, value_b, y_double;
    int index;
    
    rise = LOW_Y - HIGH_Y;
    slope = rise / RUN;
    value_b = HIGH_Y;

    cur_reps = index = 0;

    while (index <= RUN)
    {
        y_double = (slope * cur_reps) + value_b;
        printf("current repetition = %f y = %f\n", cur_reps, y_double);
        ++index;
        cur_reps = index;
    }
    printf("\n");
    
}




