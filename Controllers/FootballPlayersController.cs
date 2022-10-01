using Assignment_4_REST.Managers;
using Assignment1CAndUnitTests;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment_4_REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballPlayersController : ControllerBase
    {

        #region Properties
        private FootballPlayersManager Manager => new FootballPlayersManager();
        #endregion

        #region Constructor

        #endregion

        #region Methods
        /// <summary>
        /// Method to get a HTTPResponseCode for a response involving a collection.
        /// </summary>
        /// <param name="list">List of objects to get code for.</param>
        /// <returns>ActionResult<List<FootballPlayer>> to return for response.</returns>
        private ActionResult<List<FootballPlayer>> HTTPStatusCodeForCollection(List<FootballPlayer> list)
        {

            if (list.Count > 0) return Ok(list);
            if (list.Count <= 0) return NoContent();

            return NotFound("Given list was null.");

        }
        /// <summary>
        /// Method to get a HTTPResponseCode for a response involving an object.
        /// </summary>
        /// <param name="footballPlayer">FootballPlayer to get code for.</param>
        /// <returns>ActionResult<FootballPlayer> to return for response.</returns>
        private ActionResult<FootballPlayer> HTTPStatusCodeForObject(FootballPlayer? footballPlayer)
        {

            if (footballPlayer != null) return Ok(footballPlayer);

            return NotFound("Given object was null.");

        }

        #region Get-Methods
        /// <summary>
        /// Get-Request that returns all the FootballPlayers in the collections of the Manager.
        /// The REST-Api call can be made at: GET /api/FootballPlayers/
        /// Status200: When the list has objects.
        /// Status204: When the list has no objects.
        /// Status404: When the list doesn't exist.
        /// </summary>
        /// <returns>The collection of FootballPlayers in the Manager.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<List<FootballPlayer>> GetAll()
        {

            List<FootballPlayer> footballPlayers = Manager.GetAll();
            return HTTPStatusCodeForCollection(footballPlayers);

        }

        /// <summary>
        /// Get-Request that returns a FootballPlayer with a given id.
        /// The REST-Api call can be made at: GET /api/FootballPlayers/{id}
        /// Status200: When the object was found.
        /// Status404: When the object wasn't found.
        /// </summary>
        /// <param name="id">Id to find a FootballPlayer with.</param>
        /// <returns>The FootballPlayer-object found in the Manager.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<FootballPlayer> Get(int id)
        {

            try
            {

                FootballPlayer? footballPlayer = Manager.GetByID(id);
                return HTTPStatusCodeForObject(footballPlayer);

            } catch(Exception ex)
            {

                return NotFound(ex.Message);

            }

        }
        #endregion

        #region Post-Methods
        /// <summary>
        /// Post-Request that adds a FootballPlayer-object, given by the body of the request.
        /// The REST-Api call can be made at: POST /api/FootballPlayers/
        /// Status201: When the object was created and added to the Manager.
        /// Status400: When the given arguments was not valid for a FootballPlayer-object.
        /// </summary>
        /// <param name="footballPlayer">FootballPlayer given by the body of the request to be added.</param>
        /// <returns>The FootballPlayer-object added to the Manager.</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<FootballPlayer> Post([FromBody] FootballPlayer footballPlayer)
        {

            try
            {
                FootballPlayer? addedFootballPlayer = Manager.Add(footballPlayer);
                return Created($"/api/apibeers/{addedFootballPlayer.ID}", addedFootballPlayer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Put-Methods
        /// <summary>
        /// Put-Request that updates a FootballPlayer-Object, given by the body of the request, and id of the URI.
        /// The REST-Api call can be made at: PUT /api/FootballPlayers/{id}
        /// Status200: When the object was updated.
        /// Status400: When the values of the footballPlayer argument was invalid. (On failed Validation)
        /// Status404: When the object to update wasn't found.
        /// </summary>
        /// <param name="id">Id to find a FootballPlayer with.</param>
        /// <param name="footballPlayer">FootballPlayer given by the body of the request to update with.</param>
        /// <returns>The FootballPlyaer-object updated in the Manager.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<FootballPlayer> Put(int id, [FromBody] FootballPlayer footballPlayer)
        {

            try
            {

                FootballPlayer updatedFootballPlayer = Manager.Update(id, footballPlayer);
                return HTTPStatusCodeForObject(updatedFootballPlayer);

            } catch(Exception ex)
            {

                if (ex is ArgumentException) return BadRequest(ex.Message);
                return NotFound(ex.Message);

            }

        }
        #endregion

        #region Delete-Methods
        /// <summary>
        /// Delete-Request that deletes a FootballPlayer with a given id.
        /// The REST-Api call can be made aat: DELETE /api/FootballPlayers/{id}
        /// Status200: The object was found, and deleted.
        /// Status404: The object wasn't found.
        /// </summary>
        /// <param name="id">Id to find a FootballPlayer with.</param>
        /// <returns>The deleted FootballPlayer-object from the Manager.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<FootballPlayer> Delete(int id)
        {

            try
            {

                FootballPlayer footballPlayer = Manager.Delete(id);
                return HTTPStatusCodeForObject(footballPlayer);

            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);

            }

        }
        #endregion

        #endregion


    }
}
