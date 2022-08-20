import React from 'react'

export const AppointmentsScreen = () => {
  return (
    <>
      <h1>Appointments Screen</h1>

      {/* search parameters */}
      <form>
        <div className='row'>
          <div className="col-6">
            <label for="dayInput" className="form-label">Dia</label>
            <input type={'date'} id={'dayInput'} className='form-control' />
          </div>
          <div className="col-6">
            <button type="submit" class="btn btn-primary">Add New</button>
          </div>
        </div>
      </form>
      <hr />

      {/* list of pacients */}
      <div className='row'>
        <div className="col-1">
          #1
        </div>
        <div className="col-4">
          Pacient Name
        </div>
        <div className="col-4">
          Medical Plan
        </div>
        <div className="col-3">
          <select className="form-select">
            <option selected>Choose...</option>
            <option value="1">One</option>
            <option value="2">Two</option>
            <option value="3">Three</option>
          </select>
        </div>
      </div>
      <hr />

      {/* summary */}
      <div className='row'>
      <div className="col-12">
          44 Pacients | $999.99
        </div>
      </div>
    </>
  )
}
