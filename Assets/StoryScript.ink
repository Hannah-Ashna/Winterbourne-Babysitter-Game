VAR prep_duration = 9
VAR current_fan_duration = 6
VAR current_blanket_duration = 3
VAR inventory_fans = 0
VAR inventory_blankets = 0
VAR helpers = 0

-> Intoduction

== Intoduction ==
Welcome to our village! I'm Mr. Trutta.
Thank you for agreeing to watch over our village eggs - it'll definitely be a rewarding job! Ready to dive in? Cause I've got your first task ready for you.
->Tutorial_Options

== Tutorial_Options ==
+ [I'm ready! Let's get started.] ->Tutorial_Begin 
+ [Hmm ... I might need a few more minutes.] ->Tutorial_Delay

== Tutorial_Begin ==
Splendid. To ensure you're well prepared for the upcoming drought, I need you to gather some resources beforehand. You can pick which task you'd like to do first.
    -> Options_Loop
    
== Tutorial_Delay ==
Ah understandable, it's a big job so I'll wait ... 
-> Tutorial_Options

== Options_Loop ==
{ 
    - prep_duration >= current_fan_duration:
        + [Let's make some fans ({current_fan_duration} Days)] -> Build_Fans(->Options_Loop)
        + [Let's make some blankets ({current_blanket_duration} Days)] -> Build_Blankets(->Options_Loop)
        
    - prep_duration < current_fan_duration && prep_duration >= current_blanket_duration:
        + [Let's make some blankets (3 Days)] -> Build_Blankets(->Options_Loop)
       
    - prep_duration == 0:
        -> Status_Update
        
    - else:
        -> Status_Update
}

== Status_Update ==
Wonderful work, looks like you've managed to collect <b>{inventory_fans}</b> fan(s) and <b>{inventory_blankets}</b> blanket(s).
-> END


== Build_Fans(-> return_to) ==
... Good choice! Anything else?
{
    - helpers > 0:
        ~ inventory_fans = inventory_fans + helpers + 1 
        ~ prep_duration = prep_duration - current_fan_duration
        -> return_to
    - else:
        ~ inventory_fans = inventory_fans + 1
        ~ prep_duration = prep_duration - current_fan_duration
        -> return_to
}

== Build_Blankets(-> return_to) ==
... Good choice! Anything else?
{
    - helpers > 0:
        ~ inventory_blankets = inventory_blankets + helpers + 1
        ~ prep_duration = prep_duration - current_blanket_duration
        -> return_to
    - else:
        ~inventory_blankets = inventory_blankets + 1
        ~ prep_duration = prep_duration - current_blanket_duration
        -> return_to
}