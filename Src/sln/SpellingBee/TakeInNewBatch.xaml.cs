using SpellingBee.Db.DbModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace SpellingBee
{
  public partial class TakeInNewBatch : Window
  {
    readonly char[] _ca = "\n\r\t .,;:".ToCharArray();  // char[] _ca = "=0987654321`+_)(&^%#@~/*[]\\{}|'\"/<>\n\r\t".ToCharArray();
    readonly A0DbContext _db = A0DbContext.Create();
    string _srcText;
    string[] _textLines;

    public TakeInNewBatch(string text)
    {
      _srcText = text.Replace("\t", " ").Replace("     ", " ").Replace("    ", " ").Replace("   ", " ").Replace("  ", " ");

      InitializeComponent();

      KeyUp += new KeyEventHandler((s, e) => { if (e.Key == Key.Escape) { Close(); } }); //tu:
      MouseLeftButtonDown += new MouseButtonEventHandler((s, e) => { DragMove(); }); //tu:
      Loaded += onLoaded;
    }

    void onLoaded(object sender, RoutedEventArgs e)
    {
      if (string.IsNullOrEmpty(_srcText))
      {
        MessageBox.Show("No suitable text in Clipboard");
        DialogResult = false;
        Close();
        return;
      }

      _db.LkuLanguages.Load();
      ((CollectionViewSource)(FindResource("lkuLanguageViewSource"))).Source = _db.LkuLanguages.Local;

      _textLines = _srcText.Split(_ca, StringSplitOptions.RemoveEmptyEntries).Where(r => r.Trim().Length > 3).ToArray();
      tbWords.Text = string.Join("\n", _textLines);
      tbBn.Text = string.Format("{0:yy.MM.dd}", DateTime.Now);
      tbBn.Focus();
    }
    void OnSave(object sender, RoutedEventArgs e)
    {
      //_db.Problems.Load(); //tu: Load+Local is much much slower!!!

      var nw = DateTime.Now;
      var ci = ((CollectionViewSource)(FindResource("lkuLanguageViewSource"))).View.CurrentItem as LkuLanguage;

      foreach (var textLine in _textLines)
      {
        if (_db.Problems.Any(p =>
          //string.Compare(p.BatchSource, tbBn.Text.Trim(), true) == 0 && //let's have duplicates but in different batches.
          string.Compare(p.ProblemText, textLine.Trim(), true) == 0))
          continue;

        _db.Problems.Add(new Problem
        {
          ProblemText = textLine.Trim(),
          BatchSource = tbBn.Text.Trim(),
          Language_ID = ci.ID,
          Notes = "from " + Environment.MachineName,
          AddedAt = nw,
          AddedBy = Environment.UserName,
        });
      }

      var recsaved = _db.SaveChanges();
      MessageBox.Show(recsaved.ToString() + " records saved.");

      DialogResult = true;
    }
    void Button_Click(object sender, RoutedEventArgs e)
    {
      _srcText = _bigballofmud;
      onLoaded(sender, e);
    }

    // http://www2.sharonherald.com/herald/nie/spellb/spelllist3.html
    const string _bigballofmud = @"
Abscess is a localized collection of pus in tissues of the body.
Accessible means easy to approach
Accommodate is to do a kindness or a favor to; oblige.
Accordion is a musical instrument.
Annulment is the declaration that annuls a marriage.
Appellate is of or pertaining to appeals.
Assassin is a murderer, especially in politics.
Atrium is the central room of an ancient Roman house.
Automaton is another word for a robot.
Baboon is a large monkey found in Africa or Arabia.
Balloon is an inflated bag.
Barbiturate is used to medicine as a sedative.
Barrette is a clasp used to hold a girl's hair in place.
Bassoon is a large woodwind instrument.
Bazaar is a marketplace, especially in the Middle East.
Beige is a very light brown.
Benign is having a kindly disposition.
Bizarre is markedly unusual.
Bobbin is a reel upon which yarn or thread is wound.
Bonsai is a tree that has been dwarfed, as by pruning.
Bough is a branch of a tree.
Bouillon is a clear usually seasoned broth.
Bouquet is a bunch of flowers.
Bursar is a treasurer or business officer.
Butte is an isolated hill or mountain rising abruptly.
Cache is a hiding place.
Calendar is a table of days and weeks in a year.
Camaraderie is good fellowship.
Campaign is a military operation for a specific objective.
Canvass is to solicit votes.
Carafe is a wide-mouthed bottle used to serve beverages.
Caribou is a large, North American deer.
Cataclysm is any violent upheaval.
Caterpillar is the larva of a butterfly or moth.
Cellist is a person who plays a cello.
Cemetery is a place set apart for graves or tombs.
Censor is an official who examines material to suppress all or part of it.
Census is an official enumeration of the population.
Chameleon is a lizard that can, as protection, change color.
Charisma is a personal quality that gives one power over a group of people.
Chauffeur is a person employed to drive an automobile.
Cliche is a trite, stereotyped expression.
Codeine is used as a sedative or analgesic.
Colloquy is a conversational exchange, dialogue.
Colonel is an officer in the Army or Air Force.
Conceit is an excessively favorable opinion of oneself.
Concessionaire is someone to whom a concession has been granted.
Conscious is being aware of one's own existence.
Consensus is a majority of opinion.
Consomme is a clear soup.
Cough is to expel air through the lungs harshly, often violently.
Counsel is advice.
Coyote is a smaller, wolf-like animal.
Crescent resembles a segment of a ring tapering to points at the ends.
Crochet is needlework with a needle having a hook at one end.
Croquet is a game played by knocking balls through metal wickets with a mallet.
Crouton is a small piece of fried or toasted bread.
Crypt is a subterranean chamber or vault.
Cuckoo is a common European bird.
Cursor is a movable symbol on a computer.
Cymbal is a concave plate that produces a sharp, ringing sound when struck, played in pairs.
Debris is the remains of things broken down or destroyed.
Deceive is to mislead by a false appearance or statement.
Deductible means capable of being deducted.
Defendant is a person against whom a charge is brought against in court.
Descender is the part of the letter that goes below the body.
Detached means not attached or separated.
Deuce is a card having two spots.
Dialogue is conversation with two or more people.
Dictionary is a book containing a selection of words or a language.
Diocese is a district under the jurisdiction of a bishop.
Diphtheria is an infectious disease.
Disappear is to cease to be seen.
Discernible means capable of being distinguished.
Distraught means deeply agitated.
Doubt is to be uncertain about.
Dough is flour or meal combined with water.
Drought is a period of dry weather.
Ecstasy is rapturous delight.
Eerie is uncanny, weird.
Elite is the choice or the best of anything.
Embarrass is to cause confusion and shame to.
Ensign is a flag or banner.
Entourage is a group of attendants or associates.
Entrepreneur  is a person who organizes or manages an enterprise.
Enzyme is one of various proteins.
Etiquette is conventional requirements to social behavior.
Eulogy is a speech in praise of a dead person.
Exacerbate is to make something worse.
Exchequer is a treasury.
Facade is the front of a building.
Farce is a light, humorous play.
Fatigue is weariness from exertion.
Faucet is a device to control the flow of liquid.
Feasible is capable of being done.
Feign is to represent fictitiously.
Feint is a movement made to deceive an opponent.
Fiery is containing fire or impetuous.
Fight is a battle or combat.
Finesse is extreme delicacy or skill in performance.
Finicky means difficult to please.
Flaunt means to display oneself conspicuously.
Fledgling is a young bird or an inexperienced person.
Floe is a sheet of floating ice.
Flour is the finely ground meal of grain.
Flourish means to thrive.
Flower is the blossom of a plant.
Flue is a passage for smoke in a chimney.
Foray is a quick raid.
Forbear is to refrain or abstain from.
Frappe is a milkshake made with ice cream.
Freight is goods or cargo transported for pay.
Fugue is a composition in music.
Furlough is a leave of absence granted to an enlisted person.
Galloping is running or moving quickly.
Gambol meanS to skip about, as in dancing.
Gauge is to determine the capacity of or to measure.
Geisha is a Japanese woman trained as a singer or dancer.
Gerbil is a small, burrowing rodent.
Gerund is a verb functioning as a noun.
Geyser is a hot spring that sends up jets of water and steam.
Gherkin is the immature fruit of a cucumber, used in pickling.
Ghoul is an evil demon, a grave-robber.
Giraffe is a long-necked African animal.
Glitch is a defect or malfunction in a plan or machine.
Glower is to look or stare at with anger.
Gnarl is a knotty protuberance on a tree.
Gnu or wildebeest is a stocky, ox-like antelope.
Goad is a stick with a pointed end.
Governor is the chief executive of a state.
Gorgeous means splendid or magnificent.
Gorilla is the largest of the anthropoid apes.
Gourmet is a connoisseur of fine food and drink.
Graffiti are markings on walls.
Grammar is the study of the way sentences are constructed.
Grotesque is odd or unnatural in appearance or shape.
Gruel is a light, thin, cooked cereal.
Guild is an organization of people with related interests.
Gypsum is a common mineral.
Hackney is a carriage or coach for hire.
Haggard is having a wasted appearance.
Hallow means to make holy.
Hallucinogen is a substance that produces hallucinations.
Halve is to divide into two equal parts.
Hangar is a shelter for airplanes.
Harangue is a scolding or intense verbal attack.
Harass is to disturb persistently.
Harbinger is a herald, or one who goes before.
Harlequin is a buffoon.
Hassle is a disorderly dispute.
Havoc is great destruction or devastation.
Hearken is to pay attention or listen to.
Hearth is the floor of a fireplace.
Heifer is a cow over one year old that has not produced a calf.
Height is the extent or distance upward.
Helix is a spiral.
Hemisphere is half of the terrestrial globe.
Heresy is opinion at odds with the accepted doctrine, esp church.
Hiatus is a break in the action.
Hideous is horrible or frightful.
Hindrance is an impeding or a stopping.
Hippopotamus is called the river horse, from Africa.
Hoax is something intended to deceive or defraud.
Hobnob is to associate on friendly terms.
Hodgepodge means a jumble.
Homily is a sermon, usually on a Biblical topic.
Honest is honorable on intentions and principles.
Honeycomb is a structure bees use to store honey, pollen and eggs.
Horizon is the line that forms the apparent border between earth and sky.
Horrendous is shockingly dreadful.
Humiliate is to cause a loss of pride or dignity.
Humongous means extraordinarily large.
Hurrah is an exclamation of joy.
Hustle is to proceed or work rapidly.
Hyacinth is a plant of the lily family.
Hybrid is the offspring of two different breeds or species.
Hygiene is the science that deals with preservation of health.
Hymn is a song in praise of God.
Hyperbole is obvious exaggeration.
Hyphen is a short line used to connect parts of compound word.
Hypocrisy is pretending to something that one doesn't believe.
Icon is a picture, image or other representation.
Illegible is hard to read or decipher because of poor handwriting.
Illicit is not legally permitted.
Illiteracy is a lack of ability to read and write.
Imbecile is a person having a mental age of seven or eight.
Impasse is a deadlock.
Impede is to retard in movement by means of obstacles.
Incense is a substance producing sweet odor when burned, used in religious ceremonies to enhance a mood.
Incessant means continuing without interruption.
Incite is to stir, encourage or urge on.
Incognito is to have one's identity concealed.
Indictment is a formal accusation in a criminal case.
Inertia is lack of motion, sluggishness.
Inevitable is something that can't be avoided.
Inflammable is capable of being set on fire.
Influenza is a viral, acute, sometimes epidemic disease.
Innate is existing in one from birth.
Innocence is without sin, the state of being innocent.
Inquisition is an official investigation, especially political or religious, without regard for individual rights.
Instinct is an inborn pattern of activity or tendency to action.
Intercede is to act in behalf of someone in difficulty.
Intravenous means within a vein.
Invincible is incapable of being conquered or defeated.
Irritable is easily irritated or annoyed.
Island is land entirely surrounded by water.
Issue is the act of sending out or putting forth.
Italicize is to print in Italic type.
Jackknife is a large pocketknife.
Jaguar is the largest cat in the western hemisphere.
Jamb is a vertical side of a doorway.
Janitor is a person employed to keep things clean.
Jaundice is a yellow discoloration of the skin or eyes.
Jealous is feeling resentment against someone because of success of advantages.
Jeer is to scoff at someone.
Jeopardy is risk to loss, harm, death or injury.
Jinx is someone supposed to bring bad luck.
Jostle is to brush against, to push or shove.
Journal is a daily record.
Judgmental is involving the use of judgment.
Judicious is discreet or prudent.
Juice is the fluid that can be extracted from something.
Junction is a place where things are joined.
Karate is a method of self-defense.
Katydid is a large, American grasshopper.
Kayak is an Eskimo canoe.
Kerosene is a mixture of hydrocarbons used as fuel, or cleaning material.
Kettle is a metal container in which to cook foods.
Khaki means dull yellowish brown.
Kidnapped means being stolen, abducted or carried off by force.
Kiln is an oven to fire pottery.
Kitchen is a room or place equipped for cooking.
Kiwi is a flightless bird of New Zealand.
Knack is a special skill or aptitude.
Knead is to work dough into a uniform mixture.
Knight is a mounted soldier in the Middle Ages.
Knowledge is acquaintance with facts, as from study or investigation.
Knuckle is a joint of the finger.
Labor is productive activity for the sake of economic gain.
Lacquer is a protective coating.
Ladle is a long-handled utensil used for dipping.
Lamb is a young sheep.
Language is a body of words common to a people.
Larceny is the wrongful taking and carrying away of the personal goods of another with the intent to convert them to the taker's own use.
Laud is to praise or extol.
Laugh is to express pleasure audibly.
League is a unit of distance, usually about 3 miles.
Lectern is a stand with a slanted top, used to hold a speech or book, etc.
Lecture is a speech read or delivered before an audience.
Legible is capable of being read or deciphered with ease.
Leisure is freedom from the demands of work.
Length is the longest extent of something from end to end.
Leopard is a large, spotted cat of Asia or Africa.
Lesion is an injury; hurt; wound.
Levy is an imposing or collecting, as a tax, by force or authority.
Liaison is the contact maintained between groups to insure concerted action or cooperation.
Libelous is maliciously defamatory.
Lieutenant is a military rank.
Lightning is a brilliant electric spark discharge.
Limousine is a large, luxurious car, especially one driven by a chauffeur.
Liquor is a distilled or spiritous beverage.
Loge in a theater is the front section of the lowest balcony.
Lubricant is a substance for lessening friction.
Lucid means easily understood; completely understandable.
Luminous is radiating or reflecting light.
Lyricist is a person who writes the lyrics for a song.
Macabre is gruesome and horrifying.
Macaroni is a small tubular pasta.
Machete is a large, heavy knife used to cut underbrush or sugar cane.
Magnificent is making a splendid appearance or show.
Mahogany is a tree or a reddish-brown color.
Maim is to cripple.
Maintenance is the act of keeping things in good order.
Malaria is a disease characterized by chills or fever, caused by the bite of a mosquito.
Malice is the desire to inflict harm on someone.
Malign is to speak harmful untruths about.
Malleable is capable of being shaped by hammering or pressure.
Manacle is a shackle for the hand; handcuffs.
Mantel is a construction framing the opening of a fireplace.
Margarine is a butter-like product made from refined vegetable oils.
Marina is a boat basin offering dockage and other service for small craft.
Maroon is a dark brownish-red.
Marriage is the social institution under which a man and woman decide to live as husband and wife by legal commitments, religious ceremonies.
Martial is inclined or disposed to war.
Martyr is a person who suffers death rather than give up religion.
Massacre is the unnecessary killing of human beings, as in a war.
Mauve is a pale bluish purple.
Mayonnaise is a thick dressing of different ingredients.
Maze is a confusing network of intercommunication paths or passages.
Meager is deficient in quantity or quality.
Mechanical is having to do with machinery.
Mediocre is of only ordinary or moderate quality.
Melancholy is a gloomy state of mind.
Memoir is a record written by a person based on personal observation.
Metaphor is a figure of speech in which a term is applied to something to which it isn't literally applicable, as in a mighty fortress is our god.
Militia is a body of citizens enrolled for military service, and called out periodically for drill.
Mirth is amusement or laughter.
Miscellaneous is of mixed character.
Mischievous is maliciously or playfully annoying.
Miscue is a mistake.
Miserable is very unhappy, uneasy or uncomfortable.
Mistletoe is a plant used in Christmas decorations.
Moccasin is a heeless shoe made entirely of soft leather.
Moderator is a person who presides over a discussion.
Modify is to alter partially, to amend.
Molasses is a thick syrup produced during the refining of sugar.
Monarch is a hereditary sovereign.
Monitor is a student appointed to assisted in the conduct of a class.
Monopoly is exclusive control of a commodity of service in a particular market.
Morale is emotional or mental condition with respect to confidence, especially in the face of hardship.
Mortgage is a conveyance of an interest in property as security for the repayment of money borrowed.
Mosquito is an insect that bites, some passing on certain diseases.
Mourn is to feel or express sorrow or grief.
Muscle is a tissue, the contraction of which produces movement.
Myriad is a very great or indefinitely great number.
Myth is a traditional or legendary story, usually concerning some being or hero, without a determinable basis of fact.
Naive means unsophisticated or ingenuous.
Nasal is of or pertaining to the nose.
Nausea is sickness at the stomach, especially when loathing food.
Necessary is being essential; indispensable.
Nectar is the secretion of a plant, which attracts insects or birds that pollinate the flower.
Nephew is a son of one's brother or sister.
Nestle is to lie close and snug.
Nicotine is an alkaloid found in tobacco and valued as an insecticide.
Noble is distinguished by rank or title.
Nocturnal is of or pertaining to the night.
Novice is a person who is new to something.
Nurture is to feed and protect.
Nutritious is providing nourishment, especially to a high degree.
Obelisk is a tapering, four-sided shaft of stone, with a pyramidal top.
Obese means very fat or overweight.
Obituary is the notice of the death of a person.
Obey is to follow the directions of someone.
Oblique means slanting; sloping.
Oblivious means unaware of.
Obvious is easily understood or recognizable.
Odious is hateful or detestable.
Ogle is to look at impertinently.
Omission is the act of omitting.
Opaque is not allowing light to pass through.
Operator is a person who runs a machine, apparatus or the like.
Orator is a public speaker.
Orchestra is a group of performers on various musical instruments.
Orchid is a flower of a plant of temperate and tropical regions.
Ordinance is a decree or command.
Outweigh is to exceed in value or influence.
Pact is an agreement, covenant or compact.
Pageant is an elaborate public spectacle.
Palace is the official residence of an exalted person.
Palate is the roof of the mouth.
Pantry is a room in which food is kept.
Papaya is a large, yellow, melonlike fruit.
Paprika is a red, powdery condiment.
Paraffin is a substance used in candles or to waterproof paper.
Parallel is extending in the same direction, but never converging.
Parcel is a small package, a bundle.
Parfait is a dessert of ice cream and fruit, or ice cream and syrup.
Partial means incomplete.
Particle is a tine or very small bit.
Patience is the quality of being patient.
Patio is an area of a house used for outdoor lounging, dining, etc.
Paws are the feet of an animal having claws.
Pedal is a foot-operated lever used for various things.
Peddle is to carry things from place to place for sale.
Pedestrian is a person who goes on foot.
Peek is to look or glance quickly or furtively.
Pension is a fixed amount other than a salary paid to a person.
Perceive is to become aware of.
Perceptive is having or showing keenness of insight, understanding or intuition.
Perplex is to cause to be puzzled over what is not understood.
Personnel is a body of persons employed in an organization or place or work.
Perturb is to disturb in mind; agitate.
Petite means short or diminutive.
Phantom is an apparition or specter.
Phase is a stage in a process of change or development.
Phrase is a series of words in grammatical construction and acting as a unit in a sentence.
Piccolo is a small flute sounding an octave higher than an ordinary flute.
Pickle is a cucumber that has been preserved in brine.
Piece is a separate or limited quantity of something.
Pizza is a flat, baked pie of Italian origin, with various ingredients and toppings.
Plague is an epidemic disease that causes high morality.
Plaque is a tablet or plate of metal, intended for use as an ornament.
Plateau is a land area having a relatively level surface considerably raised above adjoining land.
Plural pertains to more than one.
Poise is composure.
Policy is a definite course of action.
Pollute is to make foul or unclear.
Populace is the common people of a nation.
Possess is to have as belonging to one; to own.
Posterior is situated behind or at the rear of.
Potency is power or authority.
Precede is to go before.
Precinct is a district marked out for governmental purposes, or police protection.
Precious is of high price or great value.
Principal is first or highest in rank or importance.
Python is a big constricting snake.
Quartz is one of the most common minerals.
Quash is to put down or suppress.
Queer is strange or odd.
Quirk is a peculiarity.
Raccoon is an animal with a mask-like stripe across the eyes.
Racquetball is a game similar to handball, but played with a racquet.
Raise is to move to a higher position.
Ramble is to wander around in an aimless manner.
Rapport is relation or connection.
Rapture is ecstatic joy or delight.
Ravine is a narrow, steep-sided valley.
Razor is an instrument used for shaving.
Reactor is an apparatus in which a nuclear chain-reaction can be obtained.
Receipt is a written acknowledgment of having received something.
Receive is to take something into one's possession.
Recess is a temporary withdrawal from work or activity.
Recite is to repeat words from memory.
Recommend is to present as worthy of confidence or acceptance.
Reduce is to bring down to a smaller amount.
Refrigerator is a container in which items are kept cool or cold.
Refugee is a person who flees his country in time of upheaval or war.
Regular is usual or normal.
Relieve is to ease or alleviate pain, distress or anxiety.
Reservoir is a place where water is collected or stored.
Resign is to give up an office or position.
Review is going over a subject.
Rhyme is a word agreeing with another word in terminal sound.
Rhythm is movement with uniform recurrence of a beat or accent.
Ritual is a prescribed or established procedure.
Routine is a customary or usual procedure.
Rumor is a story without confirmation or certainty as to facts.
Scene is the place where some action or event occurs.
Scent is a distinctive color.
Schedule is a plan of procedure.
Scheme is an underhand plot, intrigue.
Scholar is a learned person.
Science is a branch of knowledge or study.
Scissors is a cutting instrument.
Seize is to take hold of forcibly.
Shear is to cut something.
Sheer is transparently thin.
Shepherd is a person who tends or guards sheep.
Sheriff is the law-enforcement officer of a county.
Shield is a piece of armor worn on the arm of defensive purposes.
Siege is the act of surrounding and attacking a fortified place in such a way as to isolate it from aid or supplies.
Sigh is to let out one's breath audibly.
Sight is the power or faculty of seeing.
Sign is a token or indication.
Sincere is free of deceit or falseness.
Site is the position or location of something.
Soccer is football as it is played around the world.
Source is anything from which something comes or arises.
Souvenir is a usually inexpensive reminder of a place visited.
Spaghetti is a white, starchy pasta of Italian origin.
Stationary means standing still, not moving.
Stature is the height of a human or animal body.
Straight is without a bend or angle, not curved.
Strength is the quality or state of being strong.
Successor is a person or thing that succeeds or follows.
Suede is kid or other leather finished with a soft, nappy surface.
Superintendent is a person who oversees or directs some work.
Supervisor is a person who supervises the work done by others.
Sword is a weapon consisting of a blade and a hilt.
Tablespoonful is the amount a tablespoon can hold.
Tabloid is a newspaper whose pages are about half the size of a standard-sized newspaper.
Tackle is equipment, apparatus or gear, especially for fishing.
Tailor is a person who makes, mends or alters clothing.
Tale is a narrative that relates details or some event or incident.
Talk is to communicate by speaking.
Tantrum is a violent demonstration of rage.
Tardy means late.
Tattle is to let out secrets.
Tattoo is an indelible marking on the skin.
Tease is to irritate or provoke.
Teethe means to grow or cut teeth.
Telescope is an instrument to make distant objects appear larger.
Tension is the act of stretching or straining.
Terrace is a raised level with a vertical or sloping front or sides.
Thaw is to pass from a frozen to a liquid state.
Theme is a subject of discourse or discussion.
Thief is a person who steals.
Tight is firmly or closely fixed in place.
Tissue IS as in tissue paper.
Toad is an amphibian, a close relative of the frog.
Toast is sliced bread that has been browned by heat.
Toboggan is a long, narrow, flat-bottomed sled.
Tombstone is a marker on a tomb or grave.
Tough means strong and durable.
Trample is to tread or stamp heavily.
Tricycle is a three-wheeled vehicle for children.
Typewriter is a machine to produce letters and characters on paper.
Ukulele is a small, guitar-like instrument.
Unanswerable is not capable of being answered.
Unconscious means without awareness or cognition.
Vacuum is a space entirely devoid of matter.
Vanilla IS as in the flavor.
Villain is a cruelly malicious person.
Warranty is assurance.
Weigh is to determine the force of gravity on an object by using a scale.
Weird is unearthly or uncanny.
Wield is to exercise power or authority.
Yacht is a private cruising vessel.
Yolk is the yellow substance of an egg.
 

  
  
Aardwolf is a hyena-like animal of southern and eastern Africa.
Aberration is departing from the usual course.
Abridgment is a shortened form of a book.
Abscission is sudden termination.
Acerbate is to make sour or bitter.
Aficionado is a devotee of something.
Algorithm is a set of rules for solving a problem.
Alignment is arrangement in a straight line.
Allocution is a formal speech.
Ancillary is a subordinate or subsidiary.
Apocalypse is widespread destruction or disaster.
Applique is ornamentation applied to a material.
Archetype (arkitype) is the original model.
Avenge is to exact satisfaction for.
Babushka is a woman's scarf.
Baccalaureate is a religious service held before commencement day.
Balalaika is a Russian musical instrument.
Baroque pertains to architecture and art from 17th century Italy.
Barracuda is a long, predaceous fish.
Bayou is a marshy arm of a river, usually sluggish or stagnant.
Beleaguer is to surround with troubles.
Belligerence is a hostile attitude.
Beret is a soft, visorless cap.
Bivouac is a military encampment.
Blithe is joyous, glad or cheerful.
Boatswain is a warrant officer on a warship.
Bourgeois is to be a member of the middle class.
Boutique is a small shop within a larger store.
Boutonniere is a flower worn by a man in his lapel.
Boysenberry is a blackberry-like fruit.
Buoy is a float used to mark a water channel.
Cabaret is a restaurant providing food, drink and music.
Calisthenics are gymnastic exercises.
Callous is hard or indifferent.
Camouflage is hiding oneself from one's enemy.
Cannoneer is an artilleryman.
Cantankerous is disagreeable to deal with.
Cardiopulmonary pertaining to the heart and lungs.
Carnivorous means flesh-eating.
Catastrophe is a sudden and widespread disaster.
Celerity is swiftness, speed.
Censer a container in which incense is burned.
Changeable is liable to change or to be changed.
Chaparral is a dense growth of shrubs or trees (southwest).
Commemorate is to serve as a reminder of.
Committal is an act or instance of committing.
Connoisseur is a person competent to pass critical judgment.
Convalescence is the gradual recovery to health after illness.
Cornucopia is the horn of plenty in mythology.
Corruptible is that which can be corrupted.
Crevasse is a fissure in ice or the earth.
Croissant is a rich, buttery crescent-shaped roll.
Curmudgeon is a bad-tempered, cantankerous person.
Cynic is a person who believes in selfishness as prime motivation.
Dachshund is a long, German dog.
Decaffeinate is to extract caffeine from.
Deliverance is an act or instance of delivering.
Denouement is the final resolution of the intricacies of a plot.
Diaphragm is a part of the human body.
Dichotomy is division into two parts.
Dietitian is a person who is an expert on nutrition.
Diphthong is an unsegmented gliding speech sound
Docile is easily handled or manageable.
Echo is a repetition of sound produced by reflection of sound waves.
Eclair is a finger-shaped creampuff.
Eczema is an inflammatory condition of the skin.
Effervescent is bubbling, vivacious or gay.
Eloquence is using language with fluency and aptness.
Encumbrance is something burdensome.
Exquisite is of special beauty or charm.
Extemporaneous is done without special preparation.
Facsimile is an exact copy.
Fallacious means logically unsound.
Fascinate is to attract and hold attentively.
Fauna are the animals of a region considered as a whole.
Flocculent is like a clump of wool.
Foliage is the leaves of a plant.
Forage is food for cattle or horses.
Forsythia is a shrub of the olive family.
Fraught means full of or accompanied by.
Fuchsia is a bright, purplish-red color.
Gauche means lacking in social grace.
Genre is a class of artistic endeavor having a particular form.
Germane means relevant.
Gerrymander is dividing election districts to suit one group or party.
Glockenspiel is a musical instrument.
Gnash is to grind or strike the teeth together.
Granary is a storehouse for grain.
Grippe is the former name for influenza.
Guillotine a device for execution.
Hallelujah means praise ye the Lord.
Handwrought is formed or shaped by hand, esp. metal objects.
Harebrained means giddy or reckless.
Harpsichord is a keyboard instrument, precursor of the piano.
Haughty is disdainfully proud.
Heir is a person who inherits.
Hemorrhage is a profuse discharge of blood.
Heterogeneous is different in kind, unlike.
Hoard is a supply that is carefully guarded or hidden.
Holocaust is a great or complete destruction.
Homogenize is to form by blending unlike elements.
Homonym is a word the same in spelling and sound, but different in meaning.
Horde is a large group, a multitude.
Humoresque is a musical composition of humorous character.
Hydraulic is employing water or other liquids in motion.
Hydrolysis is chemical decomposition by reacting with water.
Hypothesis is a proposition set forth to explain some occurrence.
Hysterical is of or pertaining to hysteria.
Idyll is a composition, usually describing pastoral scenes or any appealing incident, or the like.
Iguana is a large lizard native to Central and South America.
Imperceptible is very slight, gradual or subtle.
Impetuous is characterized by sudden or rash action.
Impossible means not possible or unable to be done.
Impromptu means done without previous preparation.
Incidence means the rate of change or occurrence.
Indicator is a person or thing that indicates.
Infallible is absolutely trustworthy or sure.
Inferior is lower in station, rank or degree.
Insurgence is an act of rebellion.
Interfere is to meddle in the affairs of others.
Invoice is an itemized bill for goods or services.
Iridescent is displaying a play of bright colors, like a rainbow.
Isle is a small island.
Isthmus is a narrow strip of land with water on both sides, connecting two larger strips of land.
Jackal is a wild dog of Asia and Africa.
Jacuzzi is a trade name for a whirlpool bath and related products.
Joist is a beam used to support ceilings or floors or the like.
Juxtaposition is the act of placing close together.
Kaiser is a German or Austrian emperor.
Kaleidoscope is a continually shifting pattern or scene.
Ketch is a two-masted sailing vessel.
Knave is an unprincipled or dishonest person.
Knell is the sound made by a bell rung slowly, at a death.
Knoll is a small, rounded hill.
Labyrinth is an intricate combination of paths in which it is difficult to find the exit.
Laconic is using few words, being concise.
Laggard is a lingerer; loiterer.
Lagoon is an area of shallow water separated from the sea by sandy dunes.
Laryngitis is the inflammation of the larynx.
Larynx is the structure in which the vocal cords are located.
Lavender is a pale bluish purple.
Legionnaire is a member of any legion.
Leprechaun is a dwarf or sprite in Ireland.
Liege is a Feudal lord entitled to allegiance or service.
Luau is a feast of Hawaiian food.
Luscious is highly pleasing to the taste or smell.
Lyre is a musical instrument of ancient Greece, harp-like.
Lymphatic is pertaining to, containing or conveying lymph.
Mace is a club-like, armor-breaking weapon, used in the Middle Ages.
Magnanimous is generous in forgiving insult or injury.
Magnify is to increase the apparent size of, as does a lens.
Malfeasance is wrongdoing by a public official.
Maneuver is a planned movement of troops or warships, etc.
Mantle is a loose, sleeveless cloak or cape.
Marquee is a projection above a theater entrance, usually containing the name of the feature at the theater.
Masquerade is a party of people wearing masks and other disguises.
Mature is complete in natural growth or development.
Maul is a heavy hammer.
Melee is a confused, hand-to-hand fight among several people.
Memento is a keepsake or souvenir.
Mercenary is working or acting merely for money or reward.
Mesquite is a spiny tree found in western North America.
Mettle is courage or fortitude.
Minuscule means very small.
Mirage is something illusory, without substance or reality.
Momentous is of great or far-reaching importance.
Monastery is a house occupied by usually monks.
Monocle is an eyeglass for one eye.
Morgue is a place in which bodies are kept.
Morphine is a narcotic used as a pain-killer or sedative.
Mosque is a Muslim temple or place of public worship.
Motif is a recurring subject, theme or idea.
Mousse is a sweetened dessert with whipped cream as a base.
Mozzarella is a mild, white, semi-soft Italian cheese.
Muenster is a white cheese made from whole milk.
Municipal is of or pertaining to a town or city or its government.
Mysterious is full of or involving mystery.
Mystique is an aura of mystery or mystical power surrounding a particular occupation or pursuit.
Naughty means disobedient or mischievous.
Neuter is gender that is neither masculine nor feminine.
Nickel is a coin of the U.S., 20 of which make a dollar.
Nickelodeon is an early motion-picture theater.
Nomenclature are names or terms comprising a set or system.
Nonchalant is coolly unconcerned, unexcited.
Nonpareil is having no equal.
Noxious is harmful or injurious to health.
Nuance is a subtle difference in meaning.
Nucleus is the core.
Nuisance is an obnoxious or annoying person.
Nuptial is of or pertaining to marriage or the ceremony.
Nylons are stockings worn by women.
Obnoxious is highly objectionable or offensive.
Obsolescent means passing out of use, as a word.
Occurrence is the action, fact or instance of happening.
Ocelot is a spotted, leopard-like cat, ranging from Texas to South America.
Ogre is a monster in fairy tales.
Onyx is black.
Ophthalmology is the branch of medicine dealing with anatomy, functions and diseases of the eye.
Ordnance is cannon or artillery.
Orphan is a child who has lost both parents through death.
Oscillate is to swing or move to and fro, as a pendulum.
Overwrought is extremely excited or agitated.
Oxygen is the element constituting about one-fifth of the atmosphere.
Pacifist is a person who is opposed to war or to violence of any kind.
Palette is a board with a thumb hole, used by painters to mix colors.
Palomino is a horse with a golden coat, and a white mane and tail.
Pamphlet is a short essay, generally controversial, on some subject of contemporary interest.
Pantomime is the art of conveying things through gestures, without speech.
Papacy is the office, dignity or jurisdiction of the pope.
Parable is a short story designed to illustrate some truth.
Paralysis is a loss of movement in a body part, caused by disease or injury.
Paraphernalia is apparatus necessary for a particular activity.
Parishioner is one of the inhabitants of a parish.
Parochial is of or pertaining to a parish or parishes.
Parody is a humorous imitation of a serious piece of literature.
Parquet is a floor composed of strips or blocks of wood forming a pattern.
Partition is a division into portions or shares.
Pasture is grass used to feed livestock.
Patriarch is the male head of a family or tribal line.
Patrician is a person of noble rank; an aristocrat.
Paunchy is having a large and protruding belly.
Pause is a temporary stop or rest.
Pavilion is a building used for shelter, concerts, or exhibits.
Peak is the pointed top of a mountain.
Penchant is a strong inclination or liking for something.
Penguin is a flightless bird of the Southern Hemisphere.
Penicillin is an antibiotic of low toxicity.
Penitentiary is a prison maintained for serious offenders.
Perennial is lasting for a long time; enduring.
Periphery is the external boundary of any area.
Perjury is lying under oath.
Perseverance is doggedness, steadfastness.
Persuade is to prevail on a person to do something.
Peruse is to read through with care.
Pesticide is a chemical preparation to destroy pests.
Petition is a formally drawn request.
Phalanx is a body of troops in close array.
Phenomenon is a fact or occurrence observed or observable.
Philosopher is one who offers views on profound subjects.
Phoenix is a mythical bird able to rise from its own ashes.
Physics is the science that deals with matter, energy, motion and force.
Picturesque is visually charming or quaint.
Peace is a country's condition when not involved in war.
Pinnacle is a lofty peak.
Pinafore is a child's apron.
Pixie is a fairy or sprite, especially a mischievous one.
Placard is a paperboard sign or notice.
Placebo is a pill with no medicine but used to soothe a patient.
Plaid is any fabric woven of differently colored yarns in a cross-barred pattern.
Plight is a condition or situation especially an unfavorable one.
Plumber is a person who installs and repairs piping, fixtures, etc.
Pneumonia is inflammation of the lungs with congestion.
Poignant is keenly distressing to the feelings.
Poinsettia is sometimes called the Christmas flower.
Politicize is to bring a political flavor to.
Populous means heavily populated.
Porridge is a food made of cereal, boiled to a thick consistency in water or milk.
Posse is a force armed with legal authority.
Posthumous is arising, occurring, or continuing after one's death.
Potpourri is any mixture of unrelated objects, subjects, etc.
Practitioner is a person engaged in the practice of a profession or occupation.
Prairie is a tract of grassland; a meadow.
Precise is definitely or strictly stated.
Prerogative is an exclusive right or privilege.
Prestigious is having a high reputation.
Prey is an animal hunted or seized for food.
Principle is an accepted or professed rule of action or conduct.
Pronunciation is an accepted standard of the sound and stress patterns of a syllable or word.
Psalm is a sacred song or hymn.
Psychology is the science of the mind or of mental states and processes.
Purge is to cleanse or to purify.
Quaff is to drink a beverage.
Quandary is a state of uncertainty.
Quarantine is a strict isolation.
Questionnaire is a list of questions submitted for replies.
Queue is a braid of hair or a line of people.
Quiche is a dish with cheeses and other vegetables.
Quintessence is the pure and concentrated essence of a substance.
Rabble is a disorderly crowd; mob.
Raffle is a form of a lottery.
Rambunctious is difficult to control or handle.
Rancid is having an unpleasant smell or taste.
Raspberry is the fruit of a shrub.
Ratchet is a tool.
Rationale is the fundamental reason serving to account for something.
Recede means to go or move away.
Recluse is a person who lives apart or in seclusion.
Reconnaissance is the act of reconnoitering.
Rectify is to make or set right.
Recurrence is an act of something happening again.
Reggae is a style of Jamaican popular music.
Rehearse is to practice.
Reign is the period during which a sovereign sits on a throne.
Rein is the leather strap used to control a horse.
Remembrance is a memory.
Reminiscence is the process of recalling experiences.
Requisition is the act of requiring or demanding.
Rescind is to annul or repeal.
Respondent is a person who responds or makes replies.
Resume is a summing up, a summary.
Resurrection is the act of rising from the dead.
Revise is to amend or alter.
Rhapsodic is ecstatic or extravagantly enthusiastic.
Rhetoric is bombast or the undue use of exaggeration or display.
Rhubarb is a plant of the buckwheat family.
Right is in accordance with what is good or just.
Rigor is strictness, severity or hardness.
Rotor is a rotating part of a machine.
Rouge is any of various red cosmetics for cheek and lips.
Roulette is a game of chance.
Rubella is a disease also called German measles.
Sable is an Old World weasel-like animal.
Sachet is a small bag containing perfuming powder or the like.
Sacrilegious  is pertaining to the violation of anything sacred.
Saffron is a crocus having showy, purple flowers.
Salutatorian  is the person ranking second in the graduating class.
Sanctimonious is making a hypocritical show of religious devotion.
Sapphire is a gem with a blue color.
Sarcasm  is harsh or bitter derision or irony.
Satellite is a body that revolves around a planet, a moon.
Sauerkraut is cabbage allowed to ferment until sour.
Sauna is a bath that uses dry heat to induce perspiration.
Scandalous is disgraceful or shocking behavior.
Scarab is a beetle regarded as sacred by the ancient Egyptians.
Scenario is the outline of a plot of a dramatic work.
Scepter is a rod held as an emblem of regal or imperial power.
Schizophrenia is a severe mental disorder.
Schnauzer is a German breed of medium-sized dogs.
Sciatic is pertaining to the back of the hip.
Scour is to remove dirt by hard scrubbing.
Scourge is a cause of affliction or calamity.
Scrod is a young Atlantic codfish or haddock.
Scruple is a moral standard that acts as a restraining force.
Sculptor is a person who practices the art of sculpture.
Seance is a meeting in which people try to communicate with spirits.
Seclude is to withdraw into solitude.
Seine is a fishing net.
Semaphore is an apparatus for conveying visual signals.
Sensuous means pertaining to or affecting the senses.
Separate means to keep apart or divide.
Sepulcher is a tomb, grave or burial place.
Sequoia is a large tree, aka redwood.
Sergeant is a noncommissioned officer above the rank of corporal.
Serial is anything published in short installments at regular intervals.
Sew is to join or attach by stitches.
Shackle is something used to secure the wrist, leg, etc.
Sheathe is to put a sword into a sheath.
Sheen is luster, brightness, radiance.
Shrew is a woman of violent temper and speech.
Shroud is a cloth or sheet in which a corpse is wrapped for burial.
Sierra is a chain of hills or mountains, the peaks of which suggest the teeth of a saw.
Silhouette is a two-dimensional representation of the outline of an object.
Simile is a figure of speech in which two unlike things are compared, as in she is like a rose.
Simultaneous is occurring or operating at the same time.
Singe is to burn slightly, to scorch.
Siphon is a tube bent into legs of unequal length, for getting liquid from one container to another.
Skeptic is a person who questions the validity of something.
Skew is to turn aside or swerve.
Slaughter is the killing of cattle, etc., for food.
Sleigh is a vehicle on runners, especially used over snow or ice.
Sleight is skill or dexterity.
Sleuth is a detective.
Slough (sloo) is an area of soft, muddy ground.
Sojourn is a temporary stay.
Solder is an alloy fused and applied to the joint between metal objects to unite them.
Solemn is grave or sober or mirthless.
Sovereign is a monarch or a king.
Spasm is a sudden involuntary muscular contraction.
Specter is a ghost, phantom or apparition.
Sponsor is a person who vouches for or is responsible for a person.
Squabble is to engage in a petty quarrel.
Squeak is a short, sharp, shrill cry.
Squint is to look with the eyes partly closed.
Stationery is writing paper.
Stimulus is something that incites to action or exertion.
Strait is a narrow passage of water between 2 larger bodies of water.
Straitjacket is a garment made of strong material and designed to bind the arms.
Stroganoff is a dish of meet sauteed with onions and cooked in a sauce of sour cream.
Suave is smoothly agreeable or polite.
Subpoena is the usual writ for the summoning of witnesses.
Subtle is thin, tenuous or delicate in meaning.
Succinct means expressed in few words, concise, terse.
Sufficiency is adequacy.
Suite is a number of things forming a set.
Supersede is to replace in power, or acceptance.
Supposition is something that is supposed; assumption.
Surety is security against loss or damage.
Surrey is a light carriage for four persons.
Surrogate is a person appointed to act for another; a deputy.
Surveillance is a watch kept over a person or group.
Swerve is to turn aside abruptly.
Symposium is a meeting to discuss some subject.
Synod is an assembly of church delegates.
Synonym is a word having nearly the same meaning as another.
Syntax is the study of the rules for the formation of grammatical sentences in a language.
Tabernacle is a place or house of worship.
Tableau is a picture of a scene.
Tabular means arranged into a table.
Tachometer is a machine to measure velocity or speed.
Tacky is not tasteful or fashionable.
Tact is a sense of what to say without raising offense.
Taffy is a chewy candy.
Tail is the hindmost part of an animal.
Taint is a trace of something bad or harmful.
Tally is an account or reckoning.
Tambourine is a small drum consisting of a circular frame with skin stretched over it and several pairs of metal jingles attached.
Tandem is one following or behind the other.
Tangible is capable of being touched.
Tantalize means to torment with.
Tapestry is a fabric used for wall hangings or furniture coverings.
Tassel is an ornament consisting of a bunch of threads hanging from a round knob, used on clothing or jewelry.
Taught is the past participle of teach.
Taunt means to mock.
Tawdry means showy or cheap.
Tea is something to drink.
Tee is a golfer's aid.
Technique is the manner in which the technical skills of a particular art or field of endeavor are used.
Tedious means long and tiresome.
Teeter means to move unsteadily.
Telegraph is an apparatus to send messages to a distant place.
Telepathy is communication between minds.
Telephone is an apparatus to send sound to distances.
Temblor is a tremor; earthquake.
Tempt means to entice or allure to do something often considered wrong.
Tenor is the meaning that runs through something written or spoken.
Tense is stretched tight; high-strung or nervous.
Terrain is a tract of land.
Terse is neatly or effectively concise; brief and pithy.
Tetanus is a disease, commonly called lockjaw.
Thatch is a material used to cover roofs.
Thermometer is a device for measuring temperature.
Thesaurus is a dictionary of synonyms and antonyms.
Thesis is a proposition put forth to be considered.
Thigh is between the hip and the knee.
Thimble is a small cap, worn over the fingertip to protect it when pushing a needle through a cloth in sewing.
Third is next after the second.
Thistle is a prickly plant.
Thorough is executed without negligence or omissions.
Thumb is the short, thick inner digit of the human hand.
Tier is one of a series of rows rising one behind or above another.
Tinsel is a glittering, metallic substance, usually in strips.
Titanic meaning gigantic.
Titlist is a titleholder, champion.
Tobacco is the plant used in making cigarettes.
Tongue is the movable organ in the mouth of humans.
Tonsillectomy is the operation removing one or both tonsils.
Topaz is a mineral used as a gem.
Torque is something that produces rotation.
Tout is to solicit business.
Toxicity is the degree of being poisonous.
Traceable is capable of being traced.
Trachea is the windpipe.
Trait is a distinguishing characteristic or quality.
Tranquil is calm or peaceful.
Transcend is to rise above or go beyond.
Transient means not lasting or enduring.
Translucent is letting pass through, but not clearly.
Trapeze is an apparatus consisting of a horizontal bar attached to two suspending ropes.
Trauma is a body wound or shock produced by sudden physical injury.
Trestle is a type of frame, used in railroad spans.
Trichotomy means divided into three parts.
Trivial means of little significance or importance.
Trough is a receptacle, usually for drinking from.
Troupe is a group of actors or performers, esp. travelers.
Truancy is the act of being truant or late.
Tyrannize is to exercise absolute control or power.
Ulcer is a sore on the skin.
Uncollectible means it can't be collected.
Unkempt is disheveled or messy.
Vaccinal pertaining to vaccine or vaccination.
Vague is not clearly expressed or identified.
Vaudeville is a theatrical entertainment.
Vehemence meaning ardor or fervor.
Veneer is a thin layer of wood.
Vengeance means violent revenge or getting back.
Vermicelli is a form of pasta.
Victuals are food supplies.
Viscount is a nobleman just below an earl or count.
Vogue means something in fashion.
Vying is competing or contending.
Waive is to give up or to forgo.
Whack is to strike with a sharp blow or blows.
Wheelwright is a person whose trade is to make wheels.
Wherever is in, at or to whatever place.
Wince is to draw back or tense the body.
Wrack is wreck or wreckage.
Wreak to inflict or execute as punishment or vengeance.
Wren is a small, active songbird.
Yeoman is a petty officer in a navy.
Zeppelin is a rigid airship or dirigible.
Zoological is of or pertaining to zoology.
Zucchini is a variety of summer squash.

  
  
Abaciscus is a small abacus.
Abatjour is a device, as a skylight, for diverting light into buildings.
Abattoir is a slaughterhouse.
Abecedarian is a person who is learning the letters of the alphabet.
Abeyant means temporarily suspended.
Absorbefacient means causing absorption.
Acaulescent means either stemless or without a visible stem.
Accoucheur is a person who assists during childbirth.
Acquiesce means to comply or submit.
Aerogramme is a pre-stamped, lightweight paper that folds into its own envelope.
Affenpinscher is a breed of dog with wiry hair.
Afrikaans is an official language of South Africa.
Ageusia is the loss of the sense of taste.
Albescent means to become whitish.
Alette (ahleet) is a small wing of a building.
Amethyst is a purple quartz, used as a gem.
Amphitheater is an oval or round building with an open central area.
Ampullaceous means bottle-shaped.
Anathematize means to denounce or to curse.
Androgynous means being both male and female.
Ankh is an Egyptian cross used as a symbol of enduring life.
Anopheles is a malaria-causing mosquito.
Apartheid a rigid policy of segregation in South Africa.
Appaloosa is a hardy breed of riding horse.
Aqueous is of or pertaining to water, watery.
Armoire is a large wardrobe or movable cupboard.
Arrhythmia is any interruption in the heartbeat.
Ascetic is a person who practices self-denial for religious reasons.
Aurochs is a black European wild ox, extinct since 1627.
Autocephalous is a bishop subordinate to no superior authority.
Avuncular pertains to an uncle.
Azimuth is the angle of horizontal deviation of a bearing from a standard direction.
Babiche is a thong used in making snowshoes.
Bacchanal is a drunken revelry.
Bacciferous means bearing or producing berries.
Badinage is light, playful banter.
Bagatelle is something of little value.
Bagheera is a velvet used to make evening wear and wraps.
Baize is a felt used on tops of billiard tables.
Baksheesh is a tip or gratuity _ used in the Middle East.
Banquette is a long bench with an upholstered seat.
Bassarisk is a carnivorous mammal.
Bdellium is a resin obtained from plants (dellium).
Bellwether is someone who assumes leadership.
Beneficence is the doing of good.
Beriberi is a disease caused by a vitamin B1 deficiency.
Beryllium is a hard, light metallic element.
Bight is the middle part of a rope.
Bludgeon is a short, heavy club with one heavier end.
Boudoir is a woman's private sitting room.
Bouffant means puffed out, as in a teased hairdo.
Brooch  is a clasp or ornament.
Bucolic is of or pertaining to shepherds.
Bumbershoot is a name for an umbrella.
Cacophony is a harsh discordance of sound.
Caique is a single-masted sailing vessel (Mediterranean).
Caliph is a spiritual leader of Islam.
Cantle is the hind part of a saddle.
Carillon is a set of bells sounded by manual or pedal action.
Carrefour (carrefoor) is a crossroads, a road junction.
Cartouche a surface for receiving a painted decoration.
Casque is an open conical helmet with a nose guard (medieval).
Catafalque is a raised structure on which the body of a dead person lies.
Catarrh an inflammation of a mucous membrane.
Caterwaul is to utter long wailing cries, as a cat.
Cauterize is to burn for curative purposes.
Cayuse is an Indian pony.
Ceraceous is waxlike or waxy.
Chancre is the initial lesion of infectious diseases.
Chattel is a movable item of personal property.
Chlorofluorocarbon is a substance blamed for the hole in the ozone layer.
Cirrhosis is a disease of the liver.
Claque a group of people hired to applaud an act or performer.
Colonnade a series of regularly spaced columns.
Contemn means to treat with disdain or scorn.
Continuum is a continuous extent, series or whole.
Convivial means friendly or agreeable.
Copacetic means fine, completely satisfactory.
Corporeity is materiality.
Cuirass (kwirass) is defensive armor for the torso.
Culottes are women's trousers, usually knee or calf-length.
Dahlia is a flower.
Daguerreotype is an obsolete photographic process.
Deign to think fit or to condescend.
Demagoguery are methods or practices of a demagogue.
Demesne is possession of land as one's own.
Demitasse is a small cup for serving strong black coffee.
Desiccate is to dry thoroughly.
Dhow is a sailing vessel used by Arabs.
Diaphanous means very sheer and light.
Dilettante is a person who takes up an occupation as an amusement.
Dinghy is a small boat designed as a lifeboat.
Dispossess is to put a person out of a possession, esp. property.
Dormouse is a small rodent resembling a small squirrel.
Doubloon is a former gold coin of Spain.
Dysentery is an infectious disease.
Ecru is a very light brown.
Edelweiss is a small plant growing in the Alps.
Egregious is extraordinary is some bad way.
Emphysema is an irreversible disease of the lungs.
Ephemeral is lasting a very short time.
Epiphany is a Christian festival.
Equestrienne is a woman who rides horses.
Ersatz means serving as a substitute.
Escutcheon is a shield on which a coat of arms is depicted.
Euchre is a game of cards.
Eviscerate is to disembowel.
Exsiccate is to remove the moisture from.
Facetious means not to be taken seriously.
Fasciation is the act of bandaging.
Fiefdom is the estate of a feudal lord.
Finagle is to cheat or swindle someone.
Fizgig is a firework that makes a hissing sound.
Flaccid means soft and limp.
Fluxion is an act of flowing.
Formaldehyde is a colorless, toxic gas.
Frankincense is an aromatic gum resin used as incense.
Fricassee is meat browned, stewed and served in its own sauce.
Frieze is a decorative band on an outside wall.
Fulsome means offensive to good taste.
Fusillade is a simultaneous discharge of firearms.
Gabardine is a firm, tightly woven fabric.
Gallimaufry is a hodgepodge, a jumble.
Garrote is a method of capital punishment.
Gesundheit is used to wish good health after a sneeze.
Gewgaw is a trinket or bauble.
Ghillie is a low-cut, tongueless shoe.
Glaucescent means becoming a light bluish-green.
Glyph is a pictograph or hieroglyph.
Gneiss is a metamorphic rock.
Gnome is a small being that guards the interior treasures of earth.
Gnosis is knowledge of spiritual matters.
Godet is a triangular piece of fabric inserted to give fullness.
Golliwogg is a grotesque person.
Grandiloquence is a speed that is lofty, often to being pompous.
Guillemot is a seabird of Northern waters.
Gyve is a shackle for the leg.
Habiliments is clothing.
Hackamore is a simple, looped bridle for a horse.
Halcyon means calm or peaceful.
Halophyte is a plant that thrives in saline soil.
Handbreadth is a unit of linear measure.
Hartebeest is a large African antelope.
Hegemony is leadership or predominant influence by one nation.
Hematozoon is a parasitic protozoan that lives in the blood.
Herbage (erbij) nonwoody vegetation.
Hierarchy is a system of things ranked above each other.
Hieroglyphic is the pictograph type of script used by Egypt.
Hirsute is hairy or shaggy.
Histrionic is of or pertaining to actors or acting.
Hoatzin is a blue-faced bird of the Amazon forests.
Hootenanny is a social gathering featuring folk singing.
Horripilation is also known as goose flesh or goose bumps.
Howdah is a seat placed on the back of an elephant.
Hoyden is another name for a tomboy.
Hydrangea is a plant with showy flower clusters.
Hydrofluoric is of or derived from hydrofluoric acid.
Hyena is a doglike carnivore of Africa.
Hyperpyrexia is an abnormally high fever.
Hypnotize is to put into the hypnotic state.
Hypotenuse is the side of a right triangle opposite the right angle.
Ichneumon (iknoomen) is also called the African mongoose.
Ichthyophagist is a person who subsists on fish.
Idiosyncrasy is a characteristic or habit peculiar to an individual, incident, or the like.
Impeccable is faultless or flawless.
Imprecise means not precise or exact.
Incise (insize) means to cut into.
Indubitable is something that can't be doubted.
Inexorable is unyielding or unalterable.
Ingurgitate is to swallow greedily.
Iniquitous is wicked, sinful.
Innuendo is an indirect intimation about a person or thing, usually of a derogatory nature.
Inscrutable is incapable of being investigated or analyzed.
Insomnolence is sleeplessness.
Intermezzo is a short musical entertainment, between the acts of a drama or opera.
Interregnum is the time between the close of reign and the beginning of the next.
Intransigent is refusing to agree or compromise.
Inveigle is to entice by flattery or artful talk.
Inveigh is to protest strongly or attack vehemently with words.
Irascible is easily provoked to anger.
Jalousie is a window made of glass slats.
Jambalaya is a dish of Creole origin.
Jocose is given to joking or jesting.
Juvenescent is being youthful; young.
Kepi is a French military cap.
Kinkajou is an arboreal animal of Central and South America.
Klaxon is a loud, electric horn.
Klieg is a powerful type of arc light once used in the movies.
Kowtow is to show servile deference.
Lacerate is to tear roughly; mangle.
Lachrymal is of or pertaining to tears.
Languid is lacking in vigor or vitality.
Layette is an outfit of clothing or bedding for a newborn baby.
Legerdemain is sleight of hand or trickery.
Licit is legal or lawful.
Limn is to represent in drawing or painting.
Locus is a place or a locality.
Loquacious is talking or tending to talk too freely.
Lucent means shining.
Lynx is a wildcat of Canada and the northern U.S.
Macaque is a monkey, characterized by cheek pouches and short tail.
Maelstrom is a large, powerful, or violent whirlpool.
Manatee is an aquatic mammal of coastal waters, with 2 front flippers and a broad, spoon-shaped tail.
Maraud is to make a raid for booty.
Marionette is a puppet manipulated from above by strings.
Marten is an animal of northern forests.
Mastiff is a large, powerful, short-haired dog.
Matriarch is the female head of a tribal or family line.
Mellifluous is sweetly or smoothly flowing.
Menace is something that threatens to cause harm or evil.
Mendacious is telling lies, especially habitually.
Meringue is a topping for pies, pastry, etc.
Mesmerize is to hypnotize.
Metamorphose is to change the form or nature of, to transform.
Meticulous is taking extreme care about tiny details.
Mezzanine is the lowest balcony in a theater.
Microcosm is a world in miniature.
Millenium is a period of 1,000 years.
Mnemonic is assisting or intended to assist the memory.
Mocha is a choice variety of coffee.
Monocoque  is a type of aircraft in which the shell carries most of the stresses.
Morass is a tract or low, soft, wet ground.
Morose is gloomy or sullenly ill-humored.
Mote is a small particle, as in dust.
Mottle is a diversifying spot or blotch of color.
Mufti are civilian clothes.
Multiplicand is a number to be multiplied by another.
Munificent means one who is very generous.
Myrrh is an aromatic resinous exudation from certain plants.
Naphtha (naftha) is a colorless, volatile petroleum distillate.
Narcissism is excessive self-love; vanity.
Nascent is beginning to exist or develop.
Nebulous is hazy, vague or indistinct.
Neigh is to utter the cry of a horse; to whinny.
Nemesis is something a person can't conquer.
Newt is a brilliantly colored salamander.
Nexus is a means of connection; a tie; a link.
Niche is an ornamental recess in a wall.
Nigh means near in space, time or relation.
Nihilism is total rejection of established laws and institutions.
Nonce is the present, or immediate, occasion or purpose.
Nouveau is newly or recently created, developed, or come to prominence.
Obeisance is a bow or a curtsy.
Objurgate is to denounce vehemently; to upbraid sharply.
Obloquy is abusive language aimed at a person.
Obsequious showing servile deference; fawning on someone.
Obstreperous means unruly.
Occlude is to close, shut, or stop up.
Oleaginous means having the nature or qualities of oil.
Omniscience  is infinite knowledge.
Onerous is burdensome or troublesome.
Opalesce is to exhibit a play of colors like that of the opal.
Opprobrious is outrageously disgraceful or shameful.
Ostracism is exclusion from social acceptance.
Oxymoron is a seemingly contradictory figure of speech, like cruel kindness.
Pachyderm is an elephant.
Paean is a song of praise, joy or triumph.
Panacea is a remedy for all disease or ills.
Panache is a grand or flamboyant manner.
Panegyric is a lofty oration in praise of a person or thing.
Panoply is a wide-ranging and impressive array or display.
Papyrus is a tall, aquatic plant, native to the Nile valley.
Pariah is an outcast.
Parlance is a way or manner of speaking.
Parry is to ward off a thrust, as in fencing.
Parsec is a unit of distance equal to 3.26 light-years.
Patella is the kneecap.
Peccadillo is a very minor or slight sin or offense.
Peccary is a piglike hoofed animal of north and south America.
Peignoir is a woman's dressing gown.
Pendulous is hanging down loosely.
Penultimate the next to the last.
Penury is extreme poverty.
Perfidy is a deliberate breach of trust; treachery.
Perihelion is the point at which a planet is closest to the sun.
Perniciou means ruinous; injurious; hurtful.
Perquisite is a gratuity or tip.
Perspicuous is clearly expressed or presented.
Pestle is a tool to grind substances in a mortar.
Pharmaceutical pertains to pharmacy or pharmacists.
Phlegmatic is not easily excited to action.
Pique is to affect with sharp irritation.
Piranha is a small South American fish that eat fish, animals and sometimes attack humans.
Pirogue is an American boat, a dugout.
Pirouette is a whirling about on one foot or on the points of the toes, as in ballet dancing.
Plagiarize is to take ideas and works of another and pass it off as one's own.
Platypus is a small, egg-laying animal of Australia.
Plebeian is belonging or pertaining to the common people.
Plebiscite is a direct vote of the voters of a state in regard to some important public question.
Polychrome is being of many or various colors.
Posit is to place, put or set.
Precocious is prematurely developed.
Precursor is a person or thing that precedes.
Prescience is knowledge of things before they exist or happen.
Propitious means favorable.
Psilosis means a falling out of the hair.
Psoriasis is a common skin disease.
Ptosis is a drooping of the upper eyelid.
Pulchritude is physical beauty; comeliness.
Pyrrhic is consisting of two short or unaccented syllables.
Quadriceps is a large muscle in front of the thigh.
Quadrille is a square dance for four couples.
Quay is a landing place along a body of water.
Querulous means full of complaints.
Quiescent is being at rest; quiet; inactive.
Quinella is a type of bet on horse races.
Quirt is a riding whip.
Quoin is the external solid angle of a wall.
Quotidian means daily.
Rapier is a small sword, used for thrusting.
Rappel is the way to move down a surface in mountaineering.
Rarefy is to make rare or rarer.
Raucous is harsh or strident.
Ravenous means extremely hungry.
Recalcitrant means resisting authority.
Redoubtable is that that is to be feared; formidable.
Regurgitate is to surge or rush back.
Reparable is capable of being repaired.
Repartee is a quick, witty reply.
Repugn is to oppose or refute.
Reticent is someone disposed to be silent or not to speak freely.
Reveille is a signal to alert military personal to arise.
Rheumatism is a disorder of the extremities or back, characterized by stiffness.
Rhinoceros is a large African animal.
Ricochet is the motion of an object after deflecting from another object.
Riposte is a quick, sharp return in speech or action.
Rococo is a style of architecture and decoration.
Rogue is a dishonest person, a scoundrel.
Roil is to disturb or disquiet.
Roseate is tinged with rose; rosy.
Rough is having a coarse or uneven surface.
Rouse is to bring out of a state of sleep.
Ruse is a trick.
Saboteur is a person who commits sabotage.
Saccharin is a sweetener.
Sacroiliac is the joint where the sacrum and ilium meet.
Sacrosanct is extremely sacred or inviolable.
Sagacious showing acute mental discrenment, or being shrewd.
Saguaro is a tall, horizontally branched cactus.
Salaam is a salutation meaning peace, especially in Islamic countries.
Sanguinary means full of bloodshed, bloody.
Sarsaparilla is a soft drink made from the extract of a root.
Satiate is to supply something to excess.
Satyr is a mythological character, half human and half goat or horse.
Schiffli is a large, loomlike machine for working patterns in lace.
Schism is division or disunion.
Schlock means cheap or trashy.
Schuss is a straight downhill run at high speed, skiing.
Scimitar is a curved, single-edged sword.
Scintillate is to emit sparks.
Scorpioid is resembling a scorpion.
Scrimshaw is a carved article, especially of whale ivory.
Scrivener is a scribe.
Scythe is an implement used to cut grass or grain by hand.
Sebaceous means fatty.
Secede is to withdraw formally from an alliance.
Sedulous means persevering.
Segue (segway) means to continue at once with the next musical composition.
Seismicity is the frequency of earthquakes in a given area.
Seneschal is an officer having full charge of domestic arrangements.
Sentient means conscious.
Septuagenarian is between 70 and 80 years of age.
Sequacious means following with smooth or logical regularity.
Sequester is to remove or withdraw into solitude or retirement.
Serendipity means good fortune or luck.
Serge is a twilled fabric used for clothing.
Serrate is notched on the edge like a saw.
Setaceous is bristlelike.
Severance is the act of severing or the state of being severed.
Sexton is a church official charged with taking care of the edifice and its contents.
Sheaf is a bundle of wheat, rye, etc.
Shoal is a place where a body of water is shallow.
Sibyl is a female prophet or witch.
Sieve is an instrument with a meshed bottom, used to separate materials.
Skein is a length of yarn wound on a reel for use in manufacturing
Skink is a lizard common in both the Old and New worlds.
Sleazy means contemptibly low or disreputable.
Sloe is the small, sour fruit of the blackthorn.
Sluice is an artificial channel for conducting water.
Solecism is a nonstandard or ungrammatical usage.
Soliloquy is a discourse by a person who is talking to himself.
Sorcerer is a person who practices black magic.
Sorghum is a cereal grass.
Speciesism is discrimination in favor of one species over another.
Specious is apparently good or right though lacking real merit.
Sphinx is a figure of a creature having the head of a man or animal and the body of a lion.
Spinet is a small upright piano.
Squalor is filth or misery.
Squeegee is an implement edged with rubber to remove water from windows or floors.
Staid means of settled character, not flighty.
Stanchion is an upright bar as in a window or stall.
Stiletto is a short dagger with a thick blade.
Strafe is to attack by airplanes with machine-gun fire.
Strontium is a strong, metallic element.
Strychnine is a colorless, crystalline poison.
Sturgeon is a large fish, valued as a source of caviar.
Subcutaneous means lying under the skin, as tissue.
Submersible is that which is capable of being submerged.
Suffrutescent is partially or slightly woody.
Surcease is to cease from some action; desist.
Surfeit means excess; an excessive amount.
Suzerain is a state exercising political control over a dependent state.
Svelte is gracefully slender; lithe.
Sycamore is a North American tree.
Sycophancy is self-seeking or servile flattery.
Sylph is a slender, graceful woman or girl.
Syncopate is to place accents on beats in music normally not accented.
Tabasco is a trademark name for a condiment sauce.
Tachymeter is an instrument to rapidly determine distances.
Taciturn means inclined to silence.
Tactician is a person who is adept at planning tactics.
Taffeta is a medium- or light-weight fabric.
Talisman is an object that is supposed to possess powers.
Tallow is the fatty tissue of animals.
Talon is the claw of a bird of prey.
Tamarind is the pod of a large tropical tree.
Tamburitza is a mandolinlike instrument of southern Slavic regions.
Tanager is a songbird found in North America.
Tankard is a large drinking cup.
Tapir is a three-toed animal resembling a pig in Central and South America.
Tarantella is a rapid, whirling Italian dance.
Tarantula is a large, hairy spider.
Tariff is a list showing duties imposed on imports or exports.
Tarot is a card used for fortunetelling.
Tarry is to remain or stay awhile.
Taupe (tohp) is a brownish gray.
Taut means tightly drawn.
Taw is a fancy marble used as a shooter.
Tawny is a shade of brown tinged with yellow.
Taxable means capable of being taxed.
Teak is a large East Indian tree.
Tempestuous means tumultuous or turbulent.
Tenacious means holding fast.
Tenement is a rundown, often overcrowded apartment house.
Tenet is a principle, doctrine or dogma.
Tequila is a strong liquor from Mexico.
Termagant is a violent or brawling woman.
Tern is an aquatic bird.
Terrarium is a container for land animals.
Tertiary is of the third order.
Testatrix is a woman who makes a will.
Tetrabrach is a word of four short syllables.
Thalassic is of or pertaining to seas or oceans.
Thence means from that place.
Theocracy is a form of government in which a deity is recognized as the supreme civil ruler.
Therapeutics is the branch of medicine concerned with the remedial treatment of disease.
Thoracic is of or pertaining to the thorax.
Thought is the product of mental activity.
Thrall is one who is in bondage; slave.
Threshold is the sill of a doorway.
Tiara is a jeweled coronet worn by women.
Timbre is the characteristic quality of a sound.
Tinct is to tinge or tint, as with color.
Titillate is to excite agreeably.
Tizzy is a nervous or agitated state.
Tofu is a bland, cheeselike food, high in protein content.
Tome is a large, heavy book.
Toque is a brimless and close-fitting hat for women.
Tortoise is a turtle, especially a land turtle.
Tortuous means full of twists or turns.
Toucan is a large-billed, brightly colored bird.
Toupee is a man's wig.
Tourniquet is a device for arresting bleeding.
Traduce means to slander or defame.
Traipse is to walk or go aimlessly or idly.
Travois is a transporting device pulled by an animal.
Treacle is contrived sentimentality.
Triskaidekaphobia is a fear of the number 13.
Troglodyte is a prehistoric cave dweller.
Troika is a team of 3 horses driven abreast.
Trousseau is clothing, linen, especially for a bride.
Truculent is fierce or savagely brutal.
Truncheon is a club carried by a police officer.
Tryst is an appointment to meet at a certain time and place.
Tumescent means swelling.
Ubiquitous means omnipresent or being everywhere at the same time.
Umlaut is a mark used over a vowel.
Unconscionable means unscrupulous or not guided by conscience.
Unrequited is not returned or reciprocated.
Unwonted is not customary or usual or rare.
Vacillate means to waver in opinion.
Vacuous means empty.
Variscite is a secondary mineral.
Ventriloquism means throwing the voice.
Verisimilitude means the appearance of truth.
Vertebrae are bones composing the spinal column.
Vertiginous means whirling or spinning.
Vexillology means the study of flags.
Vicissitude means a change in the course of things.
Villein was an almost free person in the middle ages.
Virescent means turning green.
Viscera are the organs in the abdominal cavity.
Viscid means sticky.
Vociferous is crying out noisily.
Vorticity is a measure of the circulation of a fluid.
Waterborne is floating or moving on water.
Whet is to sharpen by grinding or friction.
Whippoorwill is a nocturnal North American bird.
Widgeon is a common, freshwater duck.
Wildebeest is a gnu.
Wok is a pan used in cooking Chinese food.
Wraith is a visible spirit.
Xenophobe is a person who fears or hates foreigners.
Xyster is a surgical instrument for scraping bones.
Xylose is a colorless crystalline sugar.
Ytrium is a rare metallic element.
Yurt is a tentlike dwelling of the Mongols.
Zephyr is a gentle breeze.
Zinciferous means yielding or containing zinc.
Zoanthropy (zoo) is a mental disorder in which one believes oneself to be an animal.
Zoopraxiscope is an early motion projector.
Zucchetto is a skullcap worn by Roman Catholic ecclesiastics.
";

  }
}
