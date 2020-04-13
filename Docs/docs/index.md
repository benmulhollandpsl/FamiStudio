# Installation

Depending on how you download FamiStudio, you might get scary warnings the first time you try to install or run it.

## Windows

On Windows, SmartScreen might say "Windows protected your PC".

![](images/SmartScreen1.png#center)

To bypass the warning, simply click "More Info" and then "Run Anyway".
 
![](images/SmartScreen2.png#center)

## MacOS

On MacOS, GateKeeper is a bit more agressive. At first it will look like you simply cannot run it and it will give you the option to throw FamiStudio in the recycling bin.

![](images/GateKeeper1.png#center)

To bypass this warning, open the "Security and Privacy" settings and look for the warning saying that FamiStudio was blocked. 

![](images/GateKeeper2.png#center)

Click "Open Anyway" and then you will have the option to launch it.

![](images/GateKeeper3.png#center)

# Concepts

A FamiStudio project contains:

* A list of Songs
* A list of Instruments
* A list of DPCM samples

Songs are made of Patterns, which are on one of the five Channels supported by the NES. Patterns contain Notes which are played by an Instrument (DPCM samples do not require an instrument). Instruments may have some of their attributes (pitch, volume, arpeggio) modulated by Envelopes.

Most of the operations are performed with the mouse. In general:

* The **left mouse button** adds stuff, double-clicking something edits properties (songs, patterns, instruments, DPCM)
* The **right mouse button** removes stuff
* The **middle mouse button** pans when you press it and zooms and you use the mouse wheel.

If you are working on a trackpad, all actions requiring the middle mouse buttons can be done with Alt+Left click. 

# Main Window

The UI was designed to be a simple as possible, there are almost no context menus.

The main window has 4 main components:

* The Toolbar (on top)
* The Project Explorer (on the right) is where you add/remove/edit songs and instruments.
* The Sequencer (below the toolbar) is where you schedule your patterns on one of the 5 channels. It gives a high-level view of the song.
* The Piano Roll (below the sequencer) is where you edit your patterns.

![](images/MainWindow.png#center)

At any given moment there is always:

* A selected channel, in bold in the sequencer
* A selected song, in bold in the project explorer song list
* A selected instrument, in bold in the project explorer instrument list.

The sequencer and piano roll will display the information for the currently selected song. The piano roll will play notes for the currently selected instrument, and output it on the currently selected channel.

## Keyboard shortcuts

Keyboard shortcuts or special actions are always displayed as tooltips in the upper-right corner of the toolbar. Please keep an eye on it to learn new functionalities.

![](images/Tooltip.png#center)

Here is a list of useful keyboard shortcuts:

* **Space**: Play/stop the stop
    * **Ctrl+Space**: Plays from beginning of current pattern and switch to pattern loop mode.
    * **Shift+Space**: Plays from current position and reverts back to song loop mode and 
* **Home**: Seeks back to beginning of the song.
    * **Ctrl+Home**: Seeks to beginning of the current pattern.
* **Ctrl+Z**: Undo
* **Ctrl+Y**: Redo
* **Ctrl+N**: New project
* **Ctrl+S**: Save
* **Ctrl+E**: Export
* **Ctrl+O**: Open
* **Delete**: Delete selected patterns or notes.
* **Escape**: Deselects patterns or notes, stops any sound that is stuck playing.

Some keyboard shortcuts specific to the piano roll:

* **Ctrl+Click**: Adds a stop note.
* **Shift+Click**: Adds a release note.
* **S+Click** (and drag): Creates or edit a slide note.
* **A+Click**: Toggles the attack of a note.

# Toolbar

The main toolbar contains your usual stuff: file operation, undo/redo, timecode and play control.

## Playing/pausing the song

Besides the toolbar, space bar is used to play/pause the song. Ctrl-space plays in pattern loop mode, Shift-space plays in song loop mode.

## Changing the looping mode

There are 3 looping modes:

* **Song**: loops the entire song
* **Pattern**: loops at the end of the current pattern
* **None**: stops at the end of the song

## Saving the project

Clicking on the icon save the project, right-clicking is a "save as..." and will prompt you for a new filename.

## Exporting to various formats

### Wave File (*.wav)

Only a single song can be exported at a time. You can choose the sample rate, it is recommended to stick to 44.1KHz if you want the soung to be exactly as you hear it in FamiStudio. Lower sample rate might lack high frequencies.

When exporting to WAV, the song will simply play once fully, all jump effects will be ignored.

![](images/ExportWav.png#center)

### Nintendo Sound Format (*.nsf)

Export to NSF is very basic for now.

![](images/ExportNsf.png#center)

Some limitations worth mentioning:

* If the song does not use sample, the maximum song size is between 24KB and 28KB.
* If the song uses samples, the maximum song size is between 8KB and 24KB depending on the size of the samples.

Note that these size are not printed anywhere and are not related to the size of the *.fms file. Best to simply try and see if it works.

### FamiTracker Text (*.txt)

You can export songs to FamiTracker using their Text Export format.

![](images/ExportFamiTracker.png#center)

There are some limitations:

* Pitch envelopes with looping sections will be modified on export so that the looping part sums to zero. This is done to prevent pitch from drifting up/down every time the envelope looks. The reason for this is that FamiTracker's pitch envelopes are relative while FamiStudio's are absolute.
* Instruments using both pitch and arpeggio envlopes at the same time will not sound correct in FamiTracker. This is due to the vastly different way both applications handles these. FamiTracker re-triggers the pitch envelope at each arpeggio notes (probably the more sensible way), while FamiStudio simply runs both at the same time.

### FamiTone2 Assembly Code (*.s, *.asm, *.dmc)

Exporting to FamiTone2 works in the same way as the command line tools provided by Shiru.

![](images/ExportFamiTone2.png#center)

When exporting file in seperate files, you can specific a name format template for each song. The {project} and {song} macros are available.

When exporting as a single file (non-seperate), you will be prompt to name the output assembly file. If any of the exported songs uses DPCM samples, a .dmc file of the same name will also be outputted.

## Configuration dialog

Clicking the gear icon opens the configuration dialog. This dialog is fairly new and has very few settings at the moment.

### User Interface Configuration

![](images/ConfigUI.png#center)

* **Scaling**: By default, FamiStudio will use the scaling of your primary monitor on Windows (100%, 150% and 200% are support) and on macOS it will choose between 100% or 200% depending on if you have a retina display or not. This behavior can be overriden by a scaling of your choosing. This requires restarting the app.
* **Check for updates**: At startup FamiStudio checks for new version online. This can be disabled.

### Sound Configuration

![](images/ConfigSong.png#center)

* **Stop instrument after**: When instruments have release notes, there is no way for FamiStudio to know when to stop the notes. This allows stopping any sound after a specified number of seconds. This only applies to MIDI or when previewing instruments on the piano roll and has no impact on the actual song.
* **Prevent popping on square channels**: The NES had a bug where the phase of square channels will reset around some notes (A-3, A-2, D-2, A-1, F-1, D-1, and B-0 on NTSC, or A#3, A#2, D#2, A#1, F#1, D#1, and C-0 on PAL), resulting in audible clicks or pops. This option will work around that bug using the Smooth Vibrato technique by Blargg, resulting in smooth pitch changes. Note that this option will not carry over to FamiTracker if you export.

### MIDI Configuration

![](images/ConfigMIDI.png#center)

* **Device**: Allows choosing the MIDI device to use for previewing instruments.

# Project Explorer

The project explorer displays the list of songs and instruments in the current project.

![](images/ProjectExplorer.png#center)

Each instrument (except DPCM samples) has 4 buttons :

* The volume envelope
* The pitch envelope
* The arpeggio envelope
* The duty cycle, four possible settings, eight settings on VRC6 (only useful for the Square channel but has effects on some others).

If an instrument has no envelope for a particular type, it will appear dimmed.

## Adding/removing songs and instruments

You can add a song or instrument by pressing the "+" sign, and you can delete a song or instrument by right-clicking on it. Deleting an instrument will delete all notes used by that instrument. Note that there always needs to be at least one song in a project.

## Editing project properties

Double-clicking on project name (first button in the project explorer) will allow you to change its name, author and copyright information. This information are used when exporting to NSF, for example.

The project properties is also where you select your expansion audio. Expansion will add extra channels on top of the default 5 that the NES supported. Note that changing the expansion audio in a project will delete all data (patterns, notes, instrument) related to the previous expansion.

List of expansions supported (will grow in the future):

* **Konami VRC6**: Adds 2 square channels and a Sawtooth channel. The square channels are better quality than the stock square as they give more control over the duty cycle and dont suffer for the phase reset bug. The Sawtooth channel will sound slightly differently if using odd duty cycles (1, 3, 5 & 7).

![](images/EditProject.png#center)

## Editing song attributes

Double-clicking on the a song will allow you to change its name, color and other attributes. Names must be unique.

Some song properties worth mentioning:

* **Speed**: How much the timer is increment each frame, values other than 150 might create uneven notes
* **Tempo**: How many frames to wait before advancing to the next note (at least when then tempo is 150)
* **Pattern Length**: Number of notes in a pattern
* **Bar length**: Will draw a thicker line in the piano roll at every bar. Must be a divider of the pattern length. Simply a visual aid, does not affect the audio in any way
* **Song length**: The number of patterns in the song

![](images/EditSong.png#center)

## Editing song properties

Double-clicking on an instrument works similarly.

* **Relative pitch** : By default, FamiStudio's pitch envelope are absolute. Meaning that the envelope values are the pitch you are going to hear, this is especially useful for vibrato where you can draw a simple sine wave. It is sometimes useful to have relative pitch envelope to create pitches that rapidly ascend or decend (useful for bassdrum sounds). This is how FamiTracker handles pitch envelopes.

![](images/EditInstrument.png#center)

##Replacing an instrument by another

Clicking on an instrument name and dragging it over another instrument will allow you to replace all notes of the first instrument by the second. This is useful prior to deleting an instrument.

## Editing envelopes

Clicking on an envelope button will start editing it in the piano roll. The duty cycle button will cycle between the 4 possible settings: 12.5%, 25%, 50% and inverted 25% since FamiTone2 does not support duty cycle envelopes. For more info on how to edit or delete envelopes, please refer to the piano roll section.

## Copying envelopes

Clicking on an envelope button and dragging it on another instrument will copy that envelope from the first to the second. Note that unlike FamiTracker, envelopes are not explicitly shared between instruments. Identical envelopes will be combined when exporting to FamiTone2, but it is your responsibility to optimize the content and ensure that you limit the number of unique envelopes.

## Deleting envelopes

Right-clicking on the icon of an envelope deletes it.

# Sequencer

The sequencer is where you organize the high-level structure of the song: which patterns play and when they play. The thumbnails of the patterns in the sequencer are by no mean accurate.
Sequencer

## Seeking

Clicking in the timeline (header) of the sequencer will move the play position.

## Editing patterns

Clicking a pattern selects it and opens the piano roll for the current channel at the location of the pattern. Double-clicking a pattern allows renaming and changing its color (pattern names need to be unique per channel).

![](images/EditPattern.png#center)

You can select multiple patterns by right-cliking and dragging in the header bar of the Sequencer. To un-select everything, simply press Esc. When multiple patterns are selected, only the color can be edited.

![](images/PatternSelection2.png#center)

You can select multiple patterns in a rectangular grid, first select a pattern and shift-clicking to a second pattern.

![](images/SquareSelection.png#center)

## Adding/removing patterns

You can add a new pattern by left-clicking on an empty space. Right-clicking deletes.

## Moving/copying patterns

When one or multiple patterns are selected, dragging them will move them in the timeline. While dragging, holding Ctrl will copy a of the pattern(s). Note that when copying a pattern, it creates an instance of the same pattern, so modifying one instance will modify all of them.

## Cut/copy/pasting patterns

When one or multiple patterns are selected, press CTRL+C (or CTRL+X for cut). Move the selection somewhere else and paste with CTRL+V.

## Muting/soloing channels

Left-clicking on the icon of a channel (Square, triangle, noise, DPCM) will toggle mute. Right-clicking will toggle solo.

## Force display channel

Clicking the tiny square icon next to the channel name will force display it in the piano roll.

![](images/ForceDisplayButton.png#center)

Channels that are force displayed and are not the current channel will appear dimmed in the piano roll. This is useful when harmonizing between multiple channels, or editing drum patterns.

![](images/ForceDisplayPianoRoll.png#center)

# Piano Roll

The piano roll is where you editing the actual notes of the song, the instrument envelopes, as well as some special effects.

![](images/PianoRoll.png#center)

You can also use it to preview instrument by clicking on the keyboard. The currently selected instrument (in the project explorer) will play on the currently selected channel (in the sequencer).

## Seeking

Clicking in the timeline (header) of the piano roll will move the play position.

## Adding and deleting notes

Clicking a pattern in the sequencer will scroll the piano roll to its location. Left-clicking in the piano roll will add a note of the currently selected instrument. Right-clicking deletes a note.

## Stop & Release notes

Using Ctrl+click will add a stop note. Stops notes are displayed as little triangles. Although they are displayed next to the note preceding them, they actually have no pitch or instrument, they simply stop the sound. Stop notes are important because on the NES, a note will play indefinitely unless you tell it to stop.

![](images/StopNote.png#center)

Using Shift+click will add a release notes. Release notes are shown as making the note thinner and triggers the envelope to jump to the release point. Release envelopes are useful to nicely fade out a note when its release, while preserving other effects like vibrato. There is no point to adding a release note to an instrument that does not have a release envelope.

![](images/ReleaseNote.png#center)

Hovering the mouse in the piano roll will display the location and note in the toolbar. Hovering over a note will display which instrument it uses.

## Note attack

By default, notes will have an "attack" which mean they will restart their envelopes (volume, pitch, etc.) from the beginning. This is represented by the little dark rectangle on the left of each note. This can be toggled for a particular note by hold the A key and clicking on a note. Note that if a note does not use the same instrument as the previous one, the attack will still play, even if disabled. Also please note that this will generally not carry over to FamiTracker, besides specific use cases around slide notes.

In this example, the first note will have an attack, while the second one will not.

![](images/NoAttack.png#center)

Since envelopes are not resetted, this means that if a note was released, it will remain released if the subsequent notes have no attack.

![](images/NoAttackReleased.png#center)

## Slide notes

Slide notes are notes that start at a given pitch (the pitch of the note) and slowly change to hit a target pitch which is represented by where the triangle ends. In this example, the attack of the second note has been disabled.

![](images/SlideNote.png#center)

Slide notes garantees that the target pitch will be reached by the end of the note (end of the triangle) but this might happen a bit earlier than the visual repesentation suggests. Especially in the higher pitches. This is due to the fact that the pitch calculations are all integer-based (with 1-bit of fraction) and it is often impossible to get the exact required slope to reach the pitch at the exact time. This will be somewhat worse when exporting to FamiTracker since FamiTracker does not have the 1-bit of fraction FamiStudio has.

When importing from FamiTracker, all possible slide effects (1xx, 2xx, 3xx, Qxx and Rxx) will be converted to slide notes. Sometimes attack will be disabled as well to mimic the same behavior. This in an inherently imperfect process since they approaches are so different. For this reason, importing/exporting slide notes with FamiTracker should be considered a lossy process.

When exporting to FamiTracker, FamiStudio will do its very best to choose which FamiTracker effect to use. Here are the general rules:

* If the slide note and its target are within 16 semitones, Qxx/Rxx (note slide up/down) will be favored as it is the most similar effect to what we are doing.
* Otherwise, if the previous note has the same pitch as the slide note, 3xx (auto-portamento) will be used.
* Finally, if none of these conditions are satisfied, 1xx/2xx (slide up/down) will be used. This is not ideal since the pitch might not exactly match the target note.

## Selecting and editing notes

You can select notes by right-clicking and dragging in the header of the piano roll. Selected notes will appear with a thick silver border. Once notes are selected, then can be moved using the arrows keys (up, down, left and right). Holding CTRL while doing so will make the notes move by larger increments.

![](images/SelectNotes.png#center)

## Cutting, copying and pasting notes

Much like the sequencer, selcted notes can be copy (or cut) by pressing CTRL+C (or CTRL+X). You can then move the selection somewhere else and paste the notes with CTRL+V.

## Special paste

If you want to paste notes, without their associated effets or volume track (or vice-versa). You can use a "special paste" by pressing CTRL+SHIFT+V. This will open a popup dialog asking you to choose what you want to paste.

![](images/PasteSpecial.png#center)

## Editing volume tracks & effects

The effect panel can be opened by clicking the little triangle at the top-left of the piano roll. Right now, only a handful of effects are supported:

* **Volume**: The overall volume of the channel.
* **Vib Speed**: Vibrato speed, used in conjuction with vibrato depth to create a vibrato effect.
* **Vib Depth**: Vibrato depth, used in conjuction with vibrato speed to create a vibrato effect.
* **Volume**: Each channel can have a volume track.
* **Jump**: Jumps back to a previous pattern in the song. The effect value is the pattern index to jump to
* **Skip**: Skips to the next pattern, at the note specified by the effect value
* **Speed**: Changes the speed of the song

Effects are edited by selecting and effect and dragging up or down to change the value. Right-clicking on an effect value deletes it.

![](images/Effect.png#center)

## Volume track

The volume tracks dictates how loud the current channel should play. This volume is combined with volume envelope by multiplication (50% volume track x 50% envelope volume = 25% total volume). It is much more efficient to use volume envelopes wherever possible and only use volume tracks to control the global volume of the song.

![](images/VolumeTrack.png#center)

## Vibrato depth & speed.

Vibrato depth and speed are used to add vibrato to a portion of the song without having to bother creating a new instrument. Please note that vibrato will temporarely override any pitch envelope on the current instrument. When vibrato is disabled (by setting depth or speed, or both to zero), the instrument will essentially have no pitch envelope until a new note is played.

![](images/Vibrato.png#center)

The depth values for the vibrato are indentical to FamiTracker but the speeds are slightly different. The way FamiTracker implements vibrato, while clever, is flawed as it undersamples the vibrato curve at high speed, leading to aliasing which ends up with a low-frequency tone that has a "ringing" sound to it. Here is a table relating the speeds in FamiStudio and FamiTracker (this is applied automatically when importing/exporting):

FamiTracker speed | FamiTracker period | FamiStudio speed | FamiStudio period
--- | --- | --- | ---
1 | 64 | 1 | 64
2 | 32 | 2 | 32
3 | 21.3 | 3 | 21
4 | 16 | 4 | 16
5 | 12.8 | 5 | 13
6 | 10.7 | 6 | 11
7 | 9.1 | 7 | 9
8 | 8 | 8 | 8
9 | 7.1 | 9 | 7
10 | 6.4 | 10 | 6
11 | 5.8 | 10 | 6
12 | 5.3 | 11 | 5
13 | 4.9 | 11 | 5
14 | 4.6 | 11 | 5
15 | 4.3 | 12 | 4

## Editing instrument envelopes

Clicking on an envelope icon in the project explorer will open the envelope of that instrument in the piano roll. The length of the envelope can be changed by left-clicking (and potentially dragging) in the timeline of the piano roll. Setting the length of an envelope to zero will disable it.

![](images/EditEnvelope.png#center)

The loop point of an envelope can be set by right-clicking in the timeline. Volume tracks are also allowed to have release envelopes. Release envelopes are played when a release note is encountered and terminates the loop by jumping to the release point. This is useful for fading out notes smoothly. The release point is set by right-dragging from the rightmost side of the envelope.

![](images/EditEnvelopeRelease.png#center)

## Editing DPCM samples

Clicking on the little icon next to the DPCM samples in the project explorer will open the piano roll in DPCM edition mode.

![](images/EditDPCM.png#center)

Clicking anywhere on a note that does not have a DPCM sample associated will prompt you to open a .DMC file. No DMC edition tool is provided, you can use FamiTracker, RJDMC or any other tool. DPCM samples are assumed to have unique names and 2 samples with the same name will be assume to be the same. Double-clicking on an existing sample edits its pitch and toggle loop. Note that only notes between C1 and D6 are allowed to have DPCM samples.