VAR prep_duration = 21
VAR current_fan_duration = 6
VAR current_blanket_duration = 3
VAR inventory_fans = 0
VAR inventory_blankets = 0
VAR helpers = 0
VAR win_condition_active = false
VAR finale_active = false
VAR final_rounds = 0
VAR egg_LC_extinct = false
VAR egg_VU_extinct = false
VAR egg_CR_extinct = false

-> Intoduction

== Intoduction ==
"Welcome back, my friend ..." 
 -> Check_Egg_Status
    
== Check_Egg_Status ==
{
    - egg_LC_extinct == false && egg_VU_extinct == false && egg_CR_extinct == false:
    -> Okay_Drought
    - egg_LC_extinct == true && egg_VU_extinct == false && egg_CR_extinct == false:
    -> Extinct_Water_Vole
    - egg_LC_extinct == false && egg_VU_extinct == true && egg_CR_extinct == false:
    -> Extinct_Whorl_Snail
    - egg_LC_extinct == false && egg_VU_extinct == false && egg_CR_extinct == true:
    -> Extinct_Pearl_Mussel
    - else:
    -> Extinct_Multiple
}
    
    
== Options_Loop_Support ==
+ [Maybe I should get some help ... (2 Days)] ->Increase_Support
+ [I'll be fine, it's time to gather resources!] -> Options_Loop_Inventory 


== Options_Loop_Inventory ==
{ 
    - prep_duration >= current_fan_duration:
        + [Let's make some fans ({current_fan_duration} Days)] -> Build_Fans(->Options_Loop_Inventory)
        + [Let's make some blankets ({current_blanket_duration} Days)] -> Build_Blankets(->Options_Loop_Inventory)
        
    - prep_duration < current_fan_duration && prep_duration >= current_blanket_duration:
        + [Let's make some blankets (3 Days)] -> Build_Blankets(->Options_Loop_Inventory)
       
    - prep_duration == 0: -> Status_Update
        
    - else: -> Status_Update
}

== Status_Update ==
"Wonderful work, looks like you've managed to collect <b>{inventory_fans}</b> fan(s) and <b>{inventory_blankets}</b> blanket(s)."
-> Drought_Begin

== Build_Fans(-> return_to) ==
... Good choice - Fans are always handy to have!
{
    - helpers > 1:
        ~ inventory_fans = inventory_fans + helpers + 1 
        ~ prep_duration = prep_duration - current_fan_duration 
        -> return_to
    - else:
        ~ inventory_fans = inventory_fans + 1
        ~ prep_duration = prep_duration - current_fan_duration
        -> return_to
}

== Build_Blankets(-> return_to) ==
... Splendid, those look especially cozy!
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

== Increase_Support ==
~ helpers = helpers + 1
~ prep_duration = prep_duration - 2
"Great work! You've now got {helpers} helper(s) to help gather more resources."
-> Win_Condition_Check

== Win_Condition_Check ==
{
    - win_condition_active == true && final_rounds < 2:
        ~ final_rounds = final_rounds + 1
        -> Options_Loop_Inventory
    - win_condition_active == true && final_rounds == 2:
        -> Finale
    - win_condition_active == false && helpers == 4:
        -> Win_Condition_Trigger
    - else:
        -> Options_Loop_Inventory
}

== Win_Condition_Trigger ==
"I've got some good news for you!"
"While you've been busy recruiting and retaining the support of {helpers} community members..."
"... some researchers caught wind of your work and want to come by and help us build a better nursery now thanks to you!"
"It'll take some time for Dr. [] and her team to have it built for us so I'll need you to help us out for a little bit longer..."
"... I'm sure you're up to the challenge!"
~ win_condition_active = true
-> Options_Loop_Inventory


== Drought_Begin == 
"Seems like the drought is starting up, you best get to the nursery! We'll speak soon my friend ..."
-> END

== Okay_Drought ==
"That drought definitely was a challenge but I see you've persevered! Thank you for doing this ..."
"Let us not waste time, we should begin preparations for the next drought!"
-> Options_Loop_Support

== Extinct_Whorl_Snail ==
"That was a pretty unfortunate drought, I hear the Whorl's egg(s) didn't make it through this time ..."
"I'll break the news to the rest of the village."
"You should look at maybe getting some help with the resource gathering"
-> Options_Loop_Support

== Extinct_Pearl_Mussel ==
"Wow, now that was a rough one, I hear the McMussel's egg(s) didn't make it through this time ..."
"I'll break the news to the rest of the village."
"You should look at maybe getting some help with the resource gathering"
-> Options_Loop_Support

== Extinct_Water_Vole ==
"That was an especially challenging drought season, I hear the Arvicola's egg(s) didn't make it through this time ..."
"I'll break the news to the rest of the village."
"You should look at maybe getting some help with the resource gathering"
-> Options_Loop_Support

== Extinct_Multiple ==
" ... oh ... well this is quite upsetting, we've seemed to have lost quite a bit this drought season..." 
"I'll break the news to the rest of the village."
"You should look at maybe getting some help with the resource gathering"
-> Options_Loop_Support

== Finale ==
~ finale_active = true
"Spectacular work my good friend! I have some wonderful news ...  I've come to relieve you of your duties"
"I've received news that Dr. [] and her team have finished building the new nursery for us!"
"Thank you for bringing our community together and helping us get the support we needed ..."
"I hope many villages like ours will get to benefit from your babysitting expertise in the future!"
+ [Farewell, Mr. Trutta! Good luck with the new nursery] -> END
+ [You're too kind! I'll head off now, thank you Mr. Trutta!] -> END
